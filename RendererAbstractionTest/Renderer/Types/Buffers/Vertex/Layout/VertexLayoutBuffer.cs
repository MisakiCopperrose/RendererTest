using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Attributes;
using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;

public unsafe class VertexLayoutBuffer : IBuffer
{
    private readonly bgfx.VertexLayoutHandle _vertexLayoutHandle;

    public VertexLayoutBuffer()
    {
        bgfx.VertexLayout vertexLayout;
        
        _vertexLayoutHandle = bgfx.create_vertex_layout(&vertexLayout);

        VertexLayout = vertexLayout;
    }

    internal bgfx.VertexLayout VertexLayout { get; }

    public ushort Handle => _vertexLayoutHandle.idx;

    public bool HasAttribute(VertexAttributes vertexAttribute)
    {
        var layout = VertexLayout;

        return bgfx.vertex_layout_has(&layout, VertexLayoutBuilder.AttributeConverter(vertexAttribute));
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_vertex_layout(_vertexLayoutHandle);
    }

    ~VertexLayoutBuffer()
    {
        ReleaseUnmanagedResources();
    }
}