using Bgfx;
using Windowing;

namespace BgfxRenderer.Buffer.Frame;

public unsafe class FrameBufferWindowed : IFrameBuffer
{
    private string _name = string.Empty;

    public FrameBufferWindowed(IWindow window)
    {
        Handle = bgfx.create_frame_buffer_from_nwh(
            window.NativeWindowHandle(),
            (ushort)window.Width,
            (ushort)window.Height,
            bgfx.TextureFormat.Count,
            bgfx.TextureFormat.Count
        );

        CurrentParent = window;
    }

    public bgfx.FrameBufferHandle Handle { get; }

    public IWindow CurrentParent { get; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_frame_buffer_name(Handle, value, value.Length);

            _name = value;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~FrameBufferWindowed()
    {
        ReleaseUnmanagedResources();
    }
}