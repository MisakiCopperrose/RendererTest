using System.Runtime.InteropServices;

namespace Bgfx;

public static unsafe class MetalWindowTest
{
    private const string DllName = "\\external\\macOS\\libmetal-layer-glfw.dylib";

    [DllImport(DllName, EntryPoint = "cbSetupMetalLayer", CallingConvention = CallingConvention.Cdecl)]
    private static extern void* cbSetupMetalLayer(nint window);

    public static void* SetupMetalLayer(nint window) => cbSetupMetalLayer(window);
}