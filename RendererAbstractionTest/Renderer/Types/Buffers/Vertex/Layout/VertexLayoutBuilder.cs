using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Attributes;
using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;

public static unsafe class VertexLayoutBuilder
{
    public static VertexLayoutBuffer Begin(this VertexLayoutBuffer vertexLayoutBuffer)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_begin(&layout, bgfx.get_renderer_type());

        return vertexLayoutBuffer;
    }

    public static VertexLayoutBuffer AddByteAttribute(this VertexLayoutBuffer vertexLayoutBuffer,
        VertexAttributes vertexAttribute, VertexAttributeLengths vertexAttributeLength,
        bool normalised = false, bool asInt = false)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Uint8,
            normalised,
            asInt
        );

        return vertexLayoutBuffer;
    }

    public static VertexLayoutBuffer AddShortAttribute(this VertexLayoutBuffer vertexLayoutBuffer,
        VertexAttributes vertexAttribute, VertexAttributeLengths vertexAttributeLength,
        bool normalised = false, bool asInt = false)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Int16,
            normalised,
            asInt
        );

        return vertexLayoutBuffer;
    }
    
    public static VertexLayoutBuffer AddFloatAttribute(this VertexLayoutBuffer vertexLayoutBuffer,
        VertexAttributes vertexAttribute, VertexAttributeLengths vertexAttributeLength)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Float,
            false,
            false
        );

        return vertexLayoutBuffer;
    }

    public static VertexLayoutBuffer Skip(this VertexLayoutBuffer vertexLayoutBuffer, byte numberOfBytes)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_skip(
            &layout,
            numberOfBytes
        );

        return vertexLayoutBuffer;
    }
    
    public static VertexLayoutBuffer End(this VertexLayoutBuffer vertexLayoutBuffer)
    {
        var layout = vertexLayoutBuffer.VertexLayout;

        bgfx.vertex_layout_end(&layout);

        return vertexLayoutBuffer;
    }
    
    internal static bgfx.Attrib AttributeConverter(VertexAttributes vertexAttribute)
    {
        return vertexAttribute switch
        {
            VertexAttributes.Position => bgfx.Attrib.Position,
            VertexAttributes.Normal => bgfx.Attrib.Normal,
            VertexAttributes.Tangent => bgfx.Attrib.Tangent,
            VertexAttributes.BiTangent => bgfx.Attrib.Bitangent,
            VertexAttributes.Color0 => bgfx.Attrib.Color0,
            VertexAttributes.Color1 => bgfx.Attrib.Color1,
            VertexAttributes.Color2 => bgfx.Attrib.Color2,
            VertexAttributes.Color3 => bgfx.Attrib.Color3,
            VertexAttributes.Indices => bgfx.Attrib.Indices,
            VertexAttributes.Weight => bgfx.Attrib.Weight,
            VertexAttributes.TexCoord0 => bgfx.Attrib.TexCoord0,
            VertexAttributes.TexCoord1 => bgfx.Attrib.TexCoord1,
            VertexAttributes.TexCoord2 => bgfx.Attrib.TexCoord2,
            VertexAttributes.TexCoord3 => bgfx.Attrib.TexCoord3,
            VertexAttributes.TexCoord4 => bgfx.Attrib.TexCoord4,
            VertexAttributes.TexCoord5 => bgfx.Attrib.TexCoord5,
            VertexAttributes.TexCoord6 => bgfx.Attrib.TexCoord6,
            VertexAttributes.TexCoord7 => bgfx.Attrib.TexCoord7,
            _ => throw new ArgumentOutOfRangeException(nameof(vertexAttribute), vertexAttribute, null)
        };
    }
}