using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Views;

public enum DrawCallOrder
{
    Default = bgfx.ViewMode.Default,
    Sequential = bgfx.ViewMode.Sequential,
    DepthAscending = bgfx.ViewMode.DepthAscending,
    DepthDescending = bgfx.ViewMode.DepthDescending
}