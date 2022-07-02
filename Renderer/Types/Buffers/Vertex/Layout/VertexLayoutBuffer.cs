using Bgfx;

namespace Renderer.Types.Buffers.Vertex.Layout;

public unsafe class VertexLayoutBuffer : IBuffer
{
    private readonly bgfx.VertexLayoutHandle _vertexLayoutHandle;
    private bgfx.VertexLayout _vertexLayout;

    public VertexLayoutBuffer()
    {
        var layout = _vertexLayout;
        
        _vertexLayoutHandle = bgfx.create_vertex_layout(&layout);
    }

    public ushort Handle => _vertexLayoutHandle.idx;

    internal bgfx.VertexLayout VertexLayout => _vertexLayout;

    public bool HasAttribute(VertexAttributes vertexAttribute)
    {
        var layout = _vertexLayout;
        
        return bgfx.vertex_layout_has(&layout, AttributeConverter(vertexAttribute));
    }

    public VertexLayoutBuffer Begin()
    {
        var layout = _vertexLayout;

        bgfx.vertex_layout_begin(
            &layout,
            bgfx.get_renderer_type()
        );

        _vertexLayout = layout;

        return this;
    }

    public VertexLayoutBuffer AddByteAttribute(VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength, bool normalised = false, bool asInt = false)
    {
        var layout = _vertexLayout;
        
        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Uint8,
            normalised,
            asInt
        );
        
        _vertexLayout = layout;

        return this;
    }

    public VertexLayoutBuffer AddShortAttribute(VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength, bool normalised = false, bool asInt = false)
    {
        var layout = _vertexLayout;
        
        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Int16,
            normalised,
            asInt
        );
        
        _vertexLayout = layout;

        return this;
    }

    public VertexLayoutBuffer AddFloatAttribute(VertexAttributes vertexAttribute,
        VertexAttributeLengths vertexAttributeLength)
    {
        var layout = _vertexLayout;
        
        bgfx.vertex_layout_add(
            &layout,
            AttributeConverter(vertexAttribute),
            (byte)vertexAttributeLength,
            bgfx.AttribType.Float,
            false,
            false
        );
        
        _vertexLayout = layout;

        return this;
    }

    public VertexLayoutBuffer Skip(byte numberOfBytes)
    {
        var layout = _vertexLayout;
        
        bgfx.vertex_layout_skip(&layout, numberOfBytes);
        
        _vertexLayout = layout;

        return this;
    }

    public VertexLayoutBuffer End()
    {
        var layout = _vertexLayout;
        
        bgfx.vertex_layout_end(&layout);

        return this;
    }

    private static bgfx.Attrib AttributeConverter(VertexAttributes vertexAttribute)
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