using Bgfx;

namespace BgfxRenderer.Occlusion;

public class OcclusionQuery : IDisposable
{
    public OcclusionQuery()
    {
        Handle = bgfx.create_occlusion_query();
    }
    
    public bgfx.OcclusionQueryHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_occlusion_query(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~OcclusionQuery()
    {
        ReleaseUnmanagedResources();
    }
}