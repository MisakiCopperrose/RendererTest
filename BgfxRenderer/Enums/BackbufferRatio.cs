using Bgfx;

namespace BgfxRenderer.Enums;

public enum BackbufferRatio
{
    Double = bgfx.BackbufferRatio.Double,
    Full = bgfx.BackbufferRatio.Equal,
    Half = bgfx.BackbufferRatio.Half,
    Quarter = bgfx.BackbufferRatio.Quarter,
    Eighth = bgfx.BackbufferRatio.Eighth,
    Sixteenth = bgfx.BackbufferRatio.Sixteenth,
}