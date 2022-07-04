using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Resources.Textures;

public interface ITexture : IDisposable
{
    bgfx.TextureHandle Handle { get; }

    Size TextureSize { get; }

    Point TextureOffset { get; }

    ushort LayerCount { get; }

    bool MipMaps { get; }

    TextureFormat TextureFormat { get; }

    TextureFlags TextureFlags { get; }

    bool ReadOnly { get; }
}