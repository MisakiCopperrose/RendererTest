using System.Diagnostics;
using Bgfx;
using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;

namespace RendererTest;

internal static unsafe class Program
{
    private static readonly Glfw Glfw = Glfw.GetApi();

    private static WindowHandle* _windowHandle;

    internal static void Main()
    {
        Glfw.SetErrorCallback(ErrorCallback);

        if (!Glfw.Init()) throw new GlfwException("GLFW INITIALIZATION FAILED!");

        Glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);

        _windowHandle = Glfw.CreateWindow(800, 600, "BGFX Test Window", null, null);

        if (_windowHandle is null)
        {
            Glfw.Terminate();

            throw new GlfwException("GLFW WINDOW CREATION FAILED!");
        }

        var pd = GetPlatformData(_windowHandle);

        bgfx.set_platform_data(&pd);

        var init = new bgfx.Init();

        bgfx.init_ctor(&init);

        init.type = bgfx.RendererType.Count;
        init.resolution = new bgfx.Resolution
        {
            width = 800,
            height = 600,
            reset = (uint) bgfx.ResetFlags.Vsync
        };

        bgfx.init(&init);

        bgfx.set_debug((uint) bgfx.DebugFlags.Text);

        bgfx.set_view_clear(0,
            (ushort) (bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x303030ff,
            1.0f,
            0
        );

        while (!Glfw.WindowShouldClose(_windowHandle))
        {
            bgfx.set_view_rect(0, 0, 0, 800, 600);

            bgfx.touch(0);

            Debug.WriteLine("Help");

            Glfw.PollEvents();
        }

        GlfwDestroyWindow(_windowHandle);

        Glfw.Terminate();
    }

    private static void ErrorCallback(ErrorCode error, string description)
    {
        throw new GlfwException($"GLFW ERROR: {error}, {description}");
    }

    public static bgfx.PlatformData GetPlatformData(WindowHandle* windowHandle)
    {
        var glfwNativeWindowHandle = GlfwNativeWindowHandle(windowHandle, out var display);

        return new bgfx.PlatformData
        {
            ndt = display,
            nwh = glfwNativeWindowHandle,
            context = null,
            backBuffer = null,
            backBufferDS = null
        };
    }

    public static void* GlfwNativeWindowHandle(WindowHandle* windowHandle, out void* display)
    {
        var nativeWindow = new GlfwNativeWindow(Glfw, windowHandle);

        display = default;

        var window = default(void*);

        switch (nativeWindow.Kind - 1)
        {
            case NativeWindowFlags.Win32:
                window = (void*) nativeWindow.Win32!.Value.Hwnd;
                break;
            case NativeWindowFlags.X11:
                display = (void*) nativeWindow.X11!.Value.Display;
                window = (void*) nativeWindow.X11!.Value.Window;
                break;
            case NativeWindowFlags.DirectFB:
                break;
            case NativeWindowFlags.Cocoa:
                window = (void*) nativeWindow.Cocoa!.Value;
                break;
            case NativeWindowFlags.UIKit:
                break;
            case NativeWindowFlags.Wayland:
                display = (void*) nativeWindow.Wayland!.Value.Display;
                window = (void*) nativeWindow.Wayland!.Value.Surface;
                break;
            case NativeWindowFlags.WinRT:
                break;
            case NativeWindowFlags.Android:
                break;
            case NativeWindowFlags.Vivante:
                break;
            case NativeWindowFlags.OS2:
                break;
            case NativeWindowFlags.Haiku:
                break;
            case NativeWindowFlags.Glfw:
                break;
            case NativeWindowFlags.Sdl:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (window == default) 
            throw new GlfwException("Could not get window handle pointer!");

        return window;
    }

    public static void GlfwDestroyWindow(WindowHandle* windowHandle)
    {
        if (windowHandle == null)
            return;

        Glfw.DestroyWindow(windowHandle);
    }
}