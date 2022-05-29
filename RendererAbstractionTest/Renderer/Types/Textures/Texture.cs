using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public unsafe class Texture : ITexture
{
    private readonly bgfx.TextureHandle _textureHandle;
    private readonly bgfx.TextureInfo _textureInfo;

    public Texture(string path)
    {
        var data = File.ReadAllBytes(path);
        var textureInfoBuffer = new bgfx.TextureInfo();

        fixed (void* dataP = data)
        {
            var memoryP = bgfx.make_ref(dataP, (uint)(sizeof(byte) * data.Length));

            _textureHandle = bgfx.create_texture(memoryP, 0, 0, &textureInfoBuffer);

            _textureInfo = textureInfoBuffer;
        }
        
        XOffset = 0;
        YOffset = 0;
        ReadOnly = true;
    }

    public ushort Handle => _textureHandle.idx;

    public ushort Width => _textureInfo.width;

    public ushort Height => _textureInfo.height;
    
    public ushort XOffset { get; }

    public ushort YOffset { get; }

    public ushort LayerCount => _textureInfo.numLayers;

    public ushort MipMapCount => _textureInfo.numMips;

    public uint Size => _textureInfo.storageSize;

    public bool ReadOnly { get; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(_textureHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture()
    {
        ReleaseUnmanagedResources();
    }
}