using System.Runtime.InteropServices;

namespace RendererLibraries.BGFX;

public static class MetalWindowTest
{
    private const string DllName = "\\BGFX\\external\\macOS\\libMetalWindowTest.dylib";

    [DllImport(DllName, EntryPoint = "cbSetupMetalLayer", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe void* SetupMetalLayer(void* window);
}