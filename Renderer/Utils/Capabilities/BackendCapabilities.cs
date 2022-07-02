using Renderer.Enums;

namespace Renderer.Utils.Capabilities;

public record BackendCapabilities
{
    public SupportedApis SupportedApi { get; init; }
}