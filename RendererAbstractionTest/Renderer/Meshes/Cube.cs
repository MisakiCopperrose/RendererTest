using System.Numerics;
using RendererAbstractionTest.Renderer.Structs;

namespace RendererAbstractionTest.Renderer.Meshes;

public static class Cube
{
    public static readonly PosColor[] Vertices =
    {
        new()
        {
            Position = new Vector3(-1f, 1f, 1f),
            Colour = 0xff000000
        },
        new()
        {
            Position = new Vector3(1f, 1f, 1f),
            Colour = 0xff0000ff
        },
        new()
        {
            Position = new Vector3(-1f, -1f, 1f),
            Colour = 0xff00ff00
        },
        new()
        {
            Position = new Vector3(1f, -1f, 1f),
            Colour = 0xff00ffff
        },
        new()
        {
            Position = new Vector3(-1f, 1f, -1f),
            Colour = 0xffff0000
        },
        new()
        {
            Position = new Vector3(1f, 1f, -1f),
            Colour = 0xffff00ff
        },
        new()
        {
            Position = new Vector3(-1f, -1f, -1f),
            Colour = 0xffffff00
        },
        new()
        {
            Position = new Vector3(1f, -1f, -1f),
            Colour = 0xffffffff
        }
    };

    public static readonly ushort[] Indices =
    {
        0, 1, 2, // 0
        1, 3, 2,
        4, 6, 5, // 2
        5, 6, 7,
        0, 2, 4, // 4
        4, 2, 6,
        1, 5, 3, // 6
        5, 7, 3,
        0, 4, 1, // 8
        4, 5, 1,
        2, 3, 6, // 10
        6, 3, 7,
    };
}