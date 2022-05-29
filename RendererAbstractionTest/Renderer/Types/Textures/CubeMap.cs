using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public class CubeMap : ITexture
{
    private bgfx.TextureHandle _textureHandle;
    
    public ushort Handle { get; }
    
    public ushort Width { get; }
    
    public ushort Height { get; }
    
    public ushort XOffset { get; }

    public ushort YOffset { get; }
    
    public ushort LayerCount { get; }
    
    public ushort MipMapCount { get; }
    
    public uint Size { get; }

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

    ~CubeMap()
    {
        ReleaseUnmanagedResources();
    }
}