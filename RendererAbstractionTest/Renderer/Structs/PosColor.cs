using System.Numerics;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;

namespace RendererAbstractionTest.Renderer.Structs;

public struct PosColor
{
    public PosColor()
    {
        Position = Vector3.Zero;
        Colour = 0xff000000;
    }

    public static VertexLayoutBuffer VertexLayoutBuffer => new VertexLayoutBuffer()
        .Begin()
        .AddFloatAttribute(VertexAttributes.Position, VertexAttributeLengths.Vec3)
        .AddByteAttribute(VertexAttributes.Color0, VertexAttributeLengths.Vec4, true)
        .End();

    public Vector3 Position { get; set; }

    public uint Colour { get; set; }
}