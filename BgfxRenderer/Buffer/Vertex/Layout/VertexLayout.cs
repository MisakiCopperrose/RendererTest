using Bgfx;

namespace BgfxRenderer.Buffer.Vertex.Layout;

public unsafe class VertexLayout : IDisposable
{
    public VertexLayout()
    {
        var layout = new bgfx.VertexLayout();

        Handle = bgfx.create_vertex_layout(&layout);
        Buffer = layout;
    }

    public bgfx.VertexLayoutHandle Handle { get; }

    public bgfx.VertexLayout Buffer { get; set; }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_vertex_layout(Handle);
    }

    ~VertexLayout()
    {
        ReleaseUnmanagedResources();
    }
}