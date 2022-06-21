using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public interface ITexture : IDisposable
{
    public ushort Handle { get; init; }

    public bgfx.TextureFormat Format { get; init; }

    public uint Size { get; init; }

    public ushort Width { get; init; }

    public ushort Height { get; init; }
    
    public ushort Depth { get; init; }

    public ushort LayerCount { get; init; }

    public byte MipCount { get; init; }

    public byte BitsPerPixel { get; init; }

    public bool ReadOnly { get; init; }
    
    
}