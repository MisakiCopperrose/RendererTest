using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Buffer.Vertex.Layout;

public static unsafe class VertexLayoutBuilder
{
    public static VertexLayout Begin(this VertexLayout vertexLayout)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_begin(&layout, (bgfx.RendererType)RuntimeData.CurrentBackend);

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }

    public static VertexLayout AddByteAttribute(this VertexLayout vertexLayout, VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength, bool normalised = false, bool asInt = false)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_add(
            &layout,
            (bgfx.Attrib)vertexAttribute,
            (byte)vertexAttributeLength,
            bgfx.AttribType.Uint8,
            normalised,
            asInt
        );

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }
    
    public static VertexLayout AddShortAttribute(this VertexLayout vertexLayout, VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength, bool normalised = false, bool asInt = false)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_add(
            &layout,
            (bgfx.Attrib)vertexAttribute,
            (byte)vertexAttributeLength,
            bgfx.AttribType.Int16,
            normalised,
            asInt
        );

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }
    
    public static VertexLayout AddHalfAttribute(this VertexLayout vertexLayout, VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_add(
            &layout,
            (bgfx.Attrib)vertexAttribute,
            (byte)vertexAttributeLength,
            bgfx.AttribType.Half,
            false,
            false
        );

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }

    public static VertexLayout AddFloatAttribute(this VertexLayout vertexLayout, VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_add(
            &layout,
            (bgfx.Attrib)vertexAttribute,
            (byte)vertexAttributeLength,
            bgfx.AttribType.Float,
            false,
            false
        );

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }

    public static VertexLayout Skip(this VertexLayout vertexLayout, byte numberOfBytes)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_skip(&layout, numberOfBytes);

        vertexLayout.Buffer = layout;

        return vertexLayout;
    }

    public static VertexLayout End(this VertexLayout vertexLayout)
    {
        var layout = vertexLayout.Buffer;

        bgfx.vertex_layout_end(&layout);

        return vertexLayout;
    }
}