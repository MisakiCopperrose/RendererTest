using Bgfx;

namespace RendererAbstractionTest.Renderer.Enums;

public enum ResolveFlags
{
    None = (byte)bgfx.ResolveFlags.None,
    AutoMipMap = (byte)bgfx.ResolveFlags.AutoGenMips
}