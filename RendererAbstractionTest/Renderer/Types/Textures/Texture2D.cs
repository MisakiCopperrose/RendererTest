using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public unsafe class Texture2D : ITexture
{
    private bgfx.TextureHandle _textureHandle;
    private bgfx.TextureInfo* _textureInfo;

    public ushort Handle { get; init; }
    public bgfx.TextureFormat Format { get; init; }
    public uint Size { get; init; }
    public ushort Width { get; init; }
    public ushort Depth { get; init; }
    public ushort Height { get; init; }
    public ushort Layers { get; init; }
    public byte MipCount { get; init; }
    public byte BitsPerPixel { get; init; }
    public bool ReadOnly { get; init; }

    private void ReleaseUnmanagedResources()
    {
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