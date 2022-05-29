namespace RendererAbstractionTest.Renderer.Types.Textures;

public interface ITexture : IDisposable
{
    ushort Handle { get; }

    ushort Width { get; }
    
    ushort Height { get; }

    ushort XOffset { get; }

    ushort YOffset { get; }

    ushort LayerCount { get; }
    
    ushort MipMapCount { get; }

    uint Size { get; }

    bool ReadOnly { get; }

    void UpdateTexture()
    {
        if (ReadOnly)
        {
            throw new Exception("Texture not mutable!");
        }
    }
}