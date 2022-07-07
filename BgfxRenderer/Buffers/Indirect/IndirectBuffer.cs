using Bgfx;

namespace BgfxRenderer.Buffers.Indirect;

public class IndirectBuffer : IDisposable
{
    public IndirectBuffer(uint indirectCallCount)
    {
        Handle = bgfx.create_indirect_buffer(indirectCallCount);
    }
    
    public bgfx.IndirectBufferHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_indirect_buffer(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~IndirectBuffer()
    {
        ReleaseUnmanagedResources();
    }
}