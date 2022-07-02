using System.Numerics;
using Renderer.Types.Buffers.Vertex.Layout;

namespace Renderer.Structs;

public struct PosColor
{
    public PosColor()
    {
        Position = Vector3.Zero;
        Colour = Vector4.Zero;
    }

    public static VertexLayoutBuffer VertexLayoutBuffer => new VertexLayoutBuffer()
        .Begin()
        .AddFloatAttribute(VertexAttributes.Position, VertexAttributeLengths.Vec3)
        .AddFloatAttribute(VertexAttributes.Color0, VertexAttributeLengths.Vec4)
        .End();

    public Vector3 Position { get; set; }

    public Vector4 Colour { get; set; }
}