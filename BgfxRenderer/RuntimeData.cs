using BgfxRenderer.Enums;

namespace BgfxRenderer;

public static class RuntimeData
{
    public static SupportedApis CurrentBackend { get; set; } = SupportedApis.NotSupported;

    public static ConfigData CurrentConfigData { get; set; } = new();
}