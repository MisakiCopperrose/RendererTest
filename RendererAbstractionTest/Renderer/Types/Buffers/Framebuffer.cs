using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Buffers;

public class Framebuffer : IBuffer
{
    private readonly bgfx.FrameBufferHandle _frameBufferHandle;
    private string _name;

    public Framebuffer(ushort width, ushort height, string name = "")
    {
        _frameBufferHandle = bgfx.create_frame_buffer(width, height, bgfx.TextureFormat.Count, 0);
        
        bgfx.set_frame_buffer_name(_frameBufferHandle, name, name.Length);

        _name = name;
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