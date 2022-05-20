namespace RendererAbstractionTest.Renderer.Types.Textures;

public interface ITexture : IDisposable
{
    ushort Handle { get; }

    ushort Width { get; }
    
    ushort Height { get; }
    
    ushort LayerCount { get; }
    
    ushort MipMapCount { get; }
}