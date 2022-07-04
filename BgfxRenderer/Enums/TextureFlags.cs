using Bgfx;

namespace BgfxRenderer.Enums;

public enum TextureFlags : ulong
{
    ReadBack = bgfx.TextureFlags.ReadBack, 
    MsaaSample = bgfx.TextureFlags.MsaaSample,
    ComputeWrite = bgfx.TextureFlags.ComputeWrite,
    Srgb = bgfx.TextureFlags.Srgb,
    BlitDestination = bgfx.TextureFlags.BlitDst
}