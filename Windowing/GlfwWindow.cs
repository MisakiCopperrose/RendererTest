using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;

namespace Windowing;

public unsafe class GlfwWindow : IDisposable, IWindow
{
    private readonly Glfw _glfw = Glfw.GetApi();
    
    private WindowHandle* _windowHandle;

    public int Width { get; } = 1920;

    public int Height { get; } = 1080;

    public void Initialize()
    {
        _glfw.SetErrorCallback(ErrorCallback);

        if (!_glfw.Init()) throw new GlfwException("GLFW INITIALIZATION FAILED!");

        _glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);

        _windowHandle = _glfw.CreateWindow(Width, Height, "BGFX Test GlfwWindow", null, null);

        if (_windowHandle is null)
        {
            _glfw.Terminate();

            throw new GlfwException("GLFW WINDOW CREATION FAILED!");
        }
    }

    public bool WindowShouldClose => _glfw.WindowShouldClose(_windowHandle);

    public void PollEvents()
    {
        while (!WindowShouldClose)
        {
            _glfw.PollEvents();
        }
    }

    public void CloseWindow()
    {
        _glfw.SetWindowShouldClose(_windowHandle, true);
    }

    public void* NativeWindowHandle()
    {
        return NativeWindowHandle(out var unused);
    }

    public void* NativeWindowHandle(out void* display)
    {
        var nativeWindow = new GlfwNativeWindow(_glfw, _windowHandle);

        display = default;

        var window = default(void*);

        switch (nativeWindow.Kind - 1)
        {
            case NativeWindowFlags.Win32:
                window = (void*)nativeWindow.Win32!.Value.Hwnd;
                break;
            case NativeWindowFlags.X11:
                display = (void*)nativeWindow.X11!.Value.Display;
                window = (void*)nativeWindow.X11!.Value.Window;
                break;
            case NativeWindowFlags.DirectFB:
                break;
            case NativeWindowFlags.Cocoa:
                //window = MetalWindowTest.SetupMetalLayer(nativeWindow.Cocoa!.Value);
                window = (void*)nativeWindow.Cocoa!.Value;
                break;
            case NativeWindowFlags.UIKit:
                break;
            case NativeWindowFlags.Wayland:
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
            throw new GlfwException("Could not get window handle pointer!");

        return window;
    }

    private static void ErrorCallback(ErrorCode error, string description)
    {
        throw new GlfwException($"GLFW ERROR: {error}, {description}");
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();

        if (disposing)
        {
            _glfw.Dispose();
        }
    }

    private void ReleaseUnmanagedResources()
    {
        _glfw.Terminate();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~GlfwWindow()
    {
        Dispose(false);
    }
}