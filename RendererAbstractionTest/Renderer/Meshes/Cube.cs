using System.Numerics;
using RendererAbstractionTest.Renderer.Structs;

namespace RendererAbstractionTest.Renderer.Meshes;

public static class Cube
{
    public static readonly PosColor[] Vertices = {
        new()
        {
            Position = new Vector3(1, 1, 0),
            Colour = default
        },
        new()
        {
            Position = new Vector3(1, -1, 0),
            Colour = default
        },
        new()
        {
            Position = new Vector3(-1, -1, 0),
            Colour = default
        },
        new()
        {
            Position = new Vector3(-1, 1, 0),
            Colour = default
        }
    };

    public static readonly int[] Indices = {
        0, 1, 3,
        1, 2, 3
    };
}