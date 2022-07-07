using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Buffers.Vertex.Layout;

public static unsafe class VertexLayoutUtils
{
    public static bool HasAttribute(this VertexLayout vertexLayout, VertexAttributes vertexAttribute)
    {
        var layout = vertexLayout.Buffer;

        return bgfx.vertex_layout_has(&layout, (bgfx.Attrib)vertexAttribute);
    }
}