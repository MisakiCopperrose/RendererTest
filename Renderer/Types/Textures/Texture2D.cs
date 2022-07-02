using Bgfx;

namespace Renderer.Types.Textures;

public class Texture2D : ITexture
{
    public ushort Handle { get; init; }

    public bgfx.TextureFormat Format { get; init; }

    public uint Size { get; init; }

    public ushort Width { get; init; }

    public ushort Depth { get; init; }

    public ushort Height { get; init; }

    public ushort LayerCount { get; init; }

    public byte MipCount { get; init; }

    public byte BitsPerPixel { get; init; }

    public bool ReadOnly { get; init; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(new bgfx.TextureHandle
        {
            idx = Handle
        });
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