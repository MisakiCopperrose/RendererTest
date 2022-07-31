using BgfxRenderer.Enums;

namespace BgfxRenderer;

public struct RuntimeData
{
    public static SupportedApis CurrentBackend { get; set; } = SupportedApis.NotSupported;

    public const string ShaderFolder = "Shaders";
}