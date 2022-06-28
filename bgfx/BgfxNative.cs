using System.Reflection;
using System.Runtime.InteropServices;

namespace Bgfx;

public static class BgfxNative
{
#if DEBUG
    private const string DebugString = "Debug";
#elif RELEASE
    private const string DebugString = string.Empty;
#endif
    public static void SetBgfx()
    {
        NativeLibrary.SetDllImportResolver(typeof(bgfx).Assembly, Resolver);
    }

    private static IntPtr Resolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        var path = string.Empty;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.X86)
            {
                // TODO: build 32 bit version
                path = $"";
            }

            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                path = $"external\\Windows\\x64\\bgfx-shared-lib{DebugString}.dll";

            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
            {
                path = $"external\\macOS\\arm64\\libbgfx-shared-lib{DebugString}.dylib";
            }

            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                // TODO: build AMD64 version
                path = $"";
            }
        }

        return NativeLibrary.Load(path);
    }
}