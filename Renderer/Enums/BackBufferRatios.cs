using Bgfx;

namespace Renderer.Enums;

public enum BackBufferRatios
{
    Equal = bgfx.BackbufferRatio.Equal, 
    Half = bgfx.BackbufferRatio.Half,
    Quarter = bgfx.BackbufferRatio.Quarter,
    Eighth = bgfx.BackbufferRatio.Eighth,
    Sixteenth = bgfx.BackbufferRatio.Sixteenth
}