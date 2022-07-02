using Bgfx;

namespace BgfxRenderer.Buffer.Vertex.Layout;

public unsafe class VertexLayout : IDisposable
{
    public bgfx.VertexLayoutHandle Handle { get; }

    public bgfx.VertexLayout Buffer { get; set; }

    public VertexLayout()
    {
        var layout = new bgfx.VertexLayout();

        Handle = bgfx.create_vertex_layout(&layout);
        Buffer = layout;
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_vertex_layout(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~VertexLayout()
    {
        ReleaseUnmanagedResources();
    }
}