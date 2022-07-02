using Bgfx;
using Windowing;

namespace Renderer.Types.Buffers.Frame;

public unsafe class WindowFrameBuffer : IDisposable
{
    private readonly bgfx.FrameBufferHandle _frameBufferHandle;

    private string _name = string.Empty;

    public WindowFrameBuffer(IWindow window)
    {
        _frameBufferHandle = bgfx.create_frame_buffer_from_nwh(
            window.NativeWindowHandle(),
            (ushort)window.Width,
            (ushort)window.Height,
            bgfx.TextureFormat.Count,
            bgfx.TextureFormat.Count
        );

        Window = window;
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

    public IWindow Window { get; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(_frameBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~WindowFrameBuffer()
    {
        ReleaseUnmanagedResources();
    }
}