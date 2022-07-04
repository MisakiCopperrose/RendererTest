using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer;

public static class Renderer
{
    public static SupportedApis CurrentBackend { get; private set; } = SupportedApis.NotSupported;

    public static ConfigData CurrentConfigData { get; private set; } = new();
    
    public static void Initialize(TaskFactory taskFactory)
    {
        // TODO: load config file

        // TODO: index files
    }

    public static void Run()
    {
        // TODO: init bgfx backend

        // TODO: run render loop

        bgfx.shutdown();
    }
}