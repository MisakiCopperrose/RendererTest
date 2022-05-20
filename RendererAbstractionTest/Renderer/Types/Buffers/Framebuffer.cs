using RendererAbstractionTest.Renderer.Types.Textures;
using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Buffers;

public class Framebuffer : IDisposable, IBuffer
{
    private bgfx.FrameBufferHandle _frameBufferHandle;
    private bgfx.Attachment _textureAttachment;
    private string _name;

    public Framebuffer(ushort width, ushort height)
    {
        //_frameBufferHandle = bgfx.create_frame_buffer(width, height, bgfx.TextureFormat.Count);
    }

    public ITexture Texture
    {
        get; // bgfx_get_texture
        set;
    }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_frame_buffer_name(_frameBufferHandle, value, value.Length);

            _name = value;
        }
    }
    
    public ushort Handle => _frameBufferHandle.idx;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(_frameBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Framebuffer()
    {
        ReleaseUnmanagedResources();
    }
}