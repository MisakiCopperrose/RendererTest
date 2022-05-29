using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public unsafe class Texture2D : ITexture
{
    private readonly bgfx.TextureHandle _textureHandle;
    private readonly bgfx.TextureInfo _textureInfo;

    public Texture2D(ushort width, ushort height, bool hasMips, ushort numLayers)
    {
        var textureInfoBuffer = new bgfx.TextureInfo();

        _textureHandle = bgfx.create_texture_2d(
            width,
            height,
            hasMips,
            numLayers,
            bgfx.TextureFormat.Count,
            0,
            null
        );

        bgfx.calc_texture_size(
            &textureInfoBuffer,
            width,
            height,
            0,
            false,
            hasMips,
            numLayers,
            bgfx.TextureFormat.Count
        );

        _textureInfo = textureInfoBuffer;

        XOffset = 0;
        YOffset = 0;
        
        ReadOnly = false;
    }

    public Texture2D(void* data, uint size, ushort width, ushort height, bool hasMips, ushort numLayers)
    {
        var textureInfoBuffer = new bgfx.TextureInfo();

        var refData = bgfx.make_ref(data, size);

        _textureHandle = bgfx.create_texture_2d(
            width,
            height,
            hasMips,
            numLayers,
            bgfx.TextureFormat.Count,
            0,
            refData
        );

        bgfx.calc_texture_size(
            &textureInfoBuffer,
            width,
            height,
            0,
            false,
            hasMips,
            numLayers,
            bgfx.TextureFormat.Count
        );

        _textureInfo = textureInfoBuffer;

        XOffset = 0;
        YOffset = 0;
        
        ReadOnly = true;
    }

    public bool ReadOnly { get; }

    public ushort Handle => _textureHandle.idx;

    public ushort Width => _textureInfo.width;

    public ushort XOffset { get; private set; }

    public ushort YOffset { get; private set; }

    public ushort Height => _textureInfo.height;

    public ushort LayerCount => _textureInfo.numLayers;

    public ushort MipMapCount => _textureInfo.numMips;

    public uint Size => _textureInfo.storageSize;

    public void UpdateTexture(void* data, uint size, ushort width, ushort height, 
        ushort xOffset, ushort yOffset, ushort numLayers, byte mipLevel = 0)
    {
        var textureInfoBuffer = new bgfx.TextureInfo();
        
        if (ReadOnly)
        {
            throw new Exception("Texture not mutable!");
        }

        var refData = bgfx.make_ref(data, size);

        bgfx.update_texture_2d(
            _textureHandle,
            numLayers,
            mipLevel,
            xOffset,
            yOffset,
            width,
            height,
            refData,
            ushort.MaxValue
        );

        bgfx.calc_texture_size(
            &textureInfoBuffer,
            width, 
            height,
            0,
            false,
            mipLevel > 0,
            numLayers, 
            bgfx.TextureFormat.Count
        );
        
        XOffset = xOffset;
        YOffset = yOffset;
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(_textureHandle);
    }

    ~Texture2D()
    {
        ReleaseUnmanagedResources();
    }
}