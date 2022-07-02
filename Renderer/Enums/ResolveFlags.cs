using Bgfx;

namespace Renderer.Enums;

public enum ResolveFlags
{
    None = (byte)bgfx.ResolveFlags.None,
    AutoMipMap = (byte)bgfx.ResolveFlags.AutoGenMips
}