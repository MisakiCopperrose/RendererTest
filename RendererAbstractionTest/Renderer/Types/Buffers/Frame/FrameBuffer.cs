namespace RendererAbstractionTest.Renderer.Types.Buffers.Frame;

public class FrameBuffer : IBuffer
{
    public FrameBuffer()
    {
        
    }
    
    public ushort Handle { get; }

    private void ReleaseUnmanagedResources()
    {
        
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~FrameBuffer()
    {
        ReleaseUnmanagedResources();
    }
}