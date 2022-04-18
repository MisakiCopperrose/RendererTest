using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;
using Bgfx;

namespace RendererTest;

public static unsafe class EntryGlfw
{
    private static readonly Glfw Glfw = Glfw.GetApi();

    public static Glfw CurrentGlfw => Glfw;

    public static WindowPlatform WindowBackend { get; private set; }
    
    public enum WindowPlatform
    {
        Win32, 
        Cocoa,
        Wayland,
        X11
    }
    
    public static void* GlfwNativeWindowHandle(WindowHandle* windowHandle, out void* display)
    {
        var nativeWindow = new GlfwNativeWindow(Glfw, windowHandle);
        
        display = default;
        
        var window = default(void*);
        
        switch (nativeWindow.Kind - 1)
        {
            case NativeWindowFlags.Win32:
                WindowBackend = WindowPlatform.Win32;
                window = (void*)nativeWindow.Win32!.Value.Hwnd;
                break;
            case NativeWindowFlags.X11:
                WindowBackend = WindowPlatform.X11;
                display = (void*)nativeWindow.X11!.Value.Display;
                window = (void*)nativeWindow.X11!.Value.Window;
                break;
            case NativeWindowFlags.DirectFB:
                break;
            case NativeWindowFlags.Cocoa:
                WindowBackend = WindowPlatform.Cocoa;
                window = (void*)nativeWindow.Cocoa!.Value;
                break;
            case NativeWindowFlags.UIKit:
                break;
            case NativeWindowFlags.Wayland:
                WindowBackend = WindowPlatform.Wayland;
                display = (void*)nativeWindow.Wayland!.Value.Display;
                window = (void*)nativeWindow.Wayland!.Value.Surface;
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
        {
            throw new GlfwException("Could not get window handle pointer!");
        }

        return window;
    }

    public static void GlfwDestroyWindow(WindowHandle* windowHandle)
    {
        if (windowHandle == null) 
            return;
        
        Glfw.DestroyWindow(windowHandle);
    }

    public static void GlfwSetWindow(WindowHandle* windowHandle)
    {
        var glfwNativeWindowHandle = GlfwNativeWindowHandle(windowHandle, out var display);
        
        var pd = new bgfx.PlatformData
        {
            ndt = display,
            nwh = glfwNativeWindowHandle,
            context = null,
            backBuffer = null,
            backBufferDS = null
        };

        bgfx.set_platform_data(&pd);
    }
}