using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public class Texture3D : ITexture
{
    private bgfx.TextureHandle _textureHandle;

    public ushort Handle { get; }
    
    public ushort Width { get; }
    
    public ushort Height { get; }
    
    public ushort LayerCount { get; }
    
    public ushort MipMapCount { get; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(_textureHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture3D()
    {
        ReleaseUnmanagedResources();
    }
}