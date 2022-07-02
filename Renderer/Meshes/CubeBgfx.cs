using System.Numerics;
using Renderer.Structs;

namespace Renderer.Meshes;

public static class CubeBgfx
{
    public static readonly PosColor[] Vertices =
    {
        new()
        {
            Position = new Vector3(-1f, 1f, 1f),
            Colour = new Vector4(1, 0, 0, 1)
        },
        new()
        {
            Position = new Vector3(1f, 1f, 1f),
            Colour = new Vector4(0, 1, 0, 1)
        },
        new()
        {
            Position = new Vector3(-1f, -1f, 1f),
            Colour = new Vector4(0, 0, 1, 1)
        },
        new()
        {
            Position = new Vector3(1f, -1f, 1f),
            Colour = new Vector4(0, 1, 0, 1)
        },
        new()
        {
            Position = new Vector3(-1f, 1f, -1f),
            Colour = new Vector4(1, 0, 0, 1)
        },
        new()
        {
            Position = new Vector3(1f, 1f, -1f),
            Colour = new Vector4(0, 1, 0, 1)
        },
        new()
        {
            Position = new Vector3(-1f, -1f, -1f),
            Colour = new Vector4(0, 0, 1, 1)
        },
        new()
        {
            Position = new Vector3(1f, -1f, -1f),
            Colour = new Vector4(0, 1, 0, 1)
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