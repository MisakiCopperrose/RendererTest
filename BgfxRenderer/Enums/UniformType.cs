using Bgfx;

namespace BgfxRenderer.Enums;

public enum UniformType
{
    Sampler = bgfx.UniformType.Sampler,
    Vec4 = bgfx.UniformType.Vec4,
    Mat3 = bgfx.UniformType.Mat3,
    Mat4 = bgfx.UniformType.Mat4
}