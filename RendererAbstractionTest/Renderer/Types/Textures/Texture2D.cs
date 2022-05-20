using RendererLibraries.BGFX;
using StbImageSharp;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public unsafe class Texture2D : ITexture
{
    private const ushort Depth = 0;

    private bgfx.TextureHandle _textureHandle;
    private bgfx.TextureInfo _textureInfo;

    public Texture2D(string path, bool mips, ushort numLayers)
    {
        var buffer = ImageResult.FromMemory(File.ReadAllBytes(path));

        bgfx.Memory* bufferData;

        fixed (void* data = buffer.Data)
        {
            bufferData = bgfx.make_ref(data, (uint)(buffer.Data.Length * sizeof(byte)));
        }

        _textureHandle = bgfx.create_texture_2d(
            (ushort)buffer.Width,
            (ushort)buffer.Height,
            mips,
            numLayers,
            bgfx.TextureFormat.Count,
            0,
            bufferData
        );

        Init((ushort)buffer.Width, (ushort)buffer.Height, mips, numLayers, false);
    }

    public Texture2D(bool mips, ushort numLayers)
    {
        _textureHandle = bgfx.create_texture_2d_scaled(
            bgfx.BackbufferRatio.Count,
            mips,
            numLayers,
            bgfx.TextureFormat.Count,
            0
        );
    }

    public ushort Handle => _textureHandle.idx;

    public ushort Width { get; private set; }

    public ushort Height { get; private set; }

    public ushort LayerCount { get; private set; }

    public ushort MipMapCount { get; private set; }
    
    public bool ReadOnly { get; private set; }

    public void UpdateTexture2D(string path, bool mips, ushort numLayers)
    {
        var buffer = ImageResult.FromMemory(File.ReadAllBytes(path));

        bgfx.Memory* bufferData;

        fixed (void* data = buffer.Data)
        {
            bufferData = bgfx.make_ref(data, (uint)(buffer.Data.Length * sizeof(byte)));
        }
    }

    public void UpdateTexture2D(void* data, bool mips, ushort numLayers)
    {
        
    }

    private void Init(ushort width, ushort height, bool mips, ushort layerCount, bool readOnly)
    {
        var textureInfoBuffer = _textureInfo;

        bgfx.calc_texture_size(
            &textureInfoBuffer,
            width,
            height,
            Depth,
            false,
            mips,
            layerCount,
            bgfx.TextureFormat.Count
        );

        _textureInfo = textureInfoBuffer;

        Width = _textureInfo.width;
        Height = _textureInfo.height;
        LayerCount = _textureInfo.numLayers;
        MipMapCount = _textureInfo.numMips;
        ReadOnly = readOnly;
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(_textureHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture2D()
    {
        ReleaseUnmanagedResources();
    }
}