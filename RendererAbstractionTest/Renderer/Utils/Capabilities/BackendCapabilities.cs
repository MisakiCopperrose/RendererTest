using RendererAbstractionTest.Renderer.Enums;

namespace RendererAbstractionTest.Renderer.Utils.Capabilities;

public record BackendCapabilities
{
    public SupportedApis SupportedApi { get; init; }
}