using Bgfx;

namespace Renderer.Enums;

public enum SupportedApis
{
    Vulkan = bgfx.RendererType.Vulkan,
    Metal = bgfx.RendererType.Metal,
    D3D9 = bgfx.RendererType.Direct3D9,
    D3D12 = bgfx.RendererType.Direct3D12,
    OpenGl = bgfx.RendererType.OpenGL,
    NotSupported = bgfx.RendererType.Noop
}