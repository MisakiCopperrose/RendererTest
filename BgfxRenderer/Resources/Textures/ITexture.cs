using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Resources.Textures;

public interface ITexture : IDisposable
{
    bgfx.TextureHandle Handle { get; }

    string Name { get; set; }

    Size TextureSize { get; }

    Point TextureOffset { get; }

    bool MipMaps { get; }

    TextureFormat TextureFormat { get; }

    TextureFlags TextureFlags { get; }

    bool ReadOnly { get; }
}