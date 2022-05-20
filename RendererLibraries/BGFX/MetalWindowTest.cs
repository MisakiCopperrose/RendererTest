using System.Runtime.InteropServices;

namespace RendererLibraries.BGFX;

public static unsafe class MetalWindowTest
{
    private const string DllName = "\\BGFX\\external\\macOS\\libmetal-layer-glfw.dylib";

    [DllImport(DllName, EntryPoint = "cbSetupMetalLayer", CallingConvention = CallingConvention.Cdecl)]
    private static extern void* cbSetupMetalLayer(nint window);

    public static void* SetupMetalLayer(nint window) => cbSetupMetalLayer(window);
}