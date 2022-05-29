using System.Numerics;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Attributes;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;

namespace RendererAbstractionTest.Renderer.Structs;

public struct PosColor
{
    public PosColor()
    {
        Position = Vector3.Zero;
        Colour = 255;
    }

    public static VertexLayoutBuffer VertexLayoutBuffer => new VertexLayoutBuffer()
        .Begin()
        .AddFloatAttribute(VertexAttributes.Position, VertexAttributeLengths.Vec3)
        .AddByteAttribute(VertexAttributes.Color0, VertexAttributeLengths.Vec, true)
        .End();

    public Vector3 Position { get; set; }

    public byte Colour { get; set; }
}