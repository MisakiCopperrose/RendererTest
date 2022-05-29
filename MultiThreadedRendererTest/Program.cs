using Bgfx;
using RendererLibraries.BGFX;
using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;

namespace MultiThreadedRendererTest;

internal static unsafe class Program
{
    private const int Width = 800;
    private const int Height = 600;
    private const int ClearView = 0;
    private const int MSecDelay = 16;

    private static readonly Glfw Glfw = Glfw.GetApi();
    private static readonly Queue<IEvent> ThreadEvents = new();

    private static WindowHandle* _windowHandle;

    private enum EventType
    {
        Exit,
        Key,
        Resize
    }

    private interface IEvent
    {
        public EventType Type { get; }
    }

    private struct ExitEvent : IEvent
    {
        public ExitEvent()
        {
            Type = EventType.Exit;
        }

        public EventType Type { get; }
    }

    private struct KeyEvent : IEvent
    {
        public KeyEvent()
        {
            Type = EventType.Key;
            Key = 0;
            Action = 0;
        }

        public EventType Type { get; }

        public Keys Key { get; set; }

        public int Action { get; set; }
    }

    private struct ResizeEvent : IEvent
    {
        public ResizeEvent()
        {
            Type = EventType.Resize;
            Width = 0;
            Height = 0;
        }

        public EventType Type { get; }

        public uint Width { get; set; }

        public uint Height { get; set; }
    }

    private static void ErrorCallback(ErrorCode error, string description)
    {
        throw new GlfwException($"GLFW ERROR: {error}, {description}");
    }

    private static void KeyCallback(WindowHandle* window, Keys key, int scancode, InputAction action, KeyModifiers mods)
    {
        
    }

    private struct ApiThreadArgs
    {
        public bgfx.PlatformData PlatformData { get; set; }

        public uint Width { get; set; }

        public uint Height { get; set; }
    }

    private static void RunApiThread(ApiThreadArgs userData)
    {
        var init = new bgfx.Init
        {
            limits = default,
            type = bgfx.RendererType.Count,
            vendorId = default,
            deviceId = default,
            capabilities = default,
            debug = default,
            profile = default,
            platformData = userData.PlatformData,
            resolution = new bgfx.Resolution
            {
                format = bgfx.TextureFormat.BC1,
                width = userData.Width,
                height = userData.Height,
                reset = (uint)bgfx.ResetFlags.Vsync,
                numBackBuffers = default,
                maxFrameLatency = default
            },
            callback = default,
            allocator = default,
        };

        if (!bgfx.init(&init))
        {
            throw new Exception("BGFX NOT INITIALISED");
        }

        bgfx.set_view_clear(
            ClearView,
            (ushort) (bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x303030ff,
            1.0f,
            0
        );

        bgfx.set_view_rect_ratio(ClearView, 0, 0, bgfx.BackbufferRatio.Equal);

        var width = userData.Width;
        var height = userData.Height;

        var showStats = false;
        var exit = false;

        while (!exit)
        {
            if (ThreadEvents.TryDequeue(out var ev))
            {
                switch (ev)
                {
                    case KeyEvent keyEvent:
                        if (keyEvent.Key == Keys.F1)
                        {
                            showStats = !showStats;
                        }

                        break;
                    case ResizeEvent resizeEvent:
                        bgfx.reset(ClearView, resizeEvent.Width, resizeEvent.Height, bgfx.TextureFormat.Count);
                        bgfx.set_view_rect_ratio(ClearView, 0, 0, bgfx.BackbufferRatio.Equal);

                        width = resizeEvent.Width;
                        height = resizeEvent.Height;
                    
                        break;
                    case ExitEvent exitEvent:
                        exit = true;
                    
                        break;
                }
            }

            bgfx.touch(ClearView);

            bgfx.dbg_text_clear(0, false);

            bgfx.set_debug((uint) (showStats ? bgfx.DebugFlags.Stats : bgfx.DebugFlags.Text));

            bgfx.frame(false);
        }
        
        bgfx.shutdown();
    }

    public static void Main(string[] args)
    {
        Glfw.SetErrorCallback(ErrorCallback);

        if (!Glfw.Init())
        {
            throw new GlfwException("GLFW DID NOT INITIALISE");
        }
        
        Glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);
        
        var window = 
            Glfw.CreateWindow(1024, 768, "Multithreaded Renderer", null, null);

        if (window is null)
        {
            throw new Exception("WINDOW COULD NOT BE CREATED");
        }

        Glfw.SetKeyCallback(window, KeyCallback);

        bgfx.render_frame(MSecDelay);

        var apiThreadArgs = new ApiThreadArgs
        {
            PlatformData = GetPlatformData(window),
            Width = Width,
            Height = Height
        };
        
        Glfw.SetWindowSize(window, Width, Height);

        var width = Width;
        var height = Height;

        var apiThread = new Task(() => RunApiThread(apiThreadArgs));
        
        apiThread.Start();

        var exit = false;

        while (!exit)
        {
            Glfw.PollEvents();
            
            if (Glfw.WindowShouldClose(window))
            {
                ThreadEvents.Enqueue(new ExitEvent());

                exit = true;
            }

            var oldWith = width;
            var oldHeight = height;
            
            Glfw.GetWindowSize(window, out width, out height);

            if (width != oldWith || height != oldHeight)
            {
                var resize = new ResizeEvent
                {
                    Width = (uint) width,
                    Height = (uint) height
                };
                
                ThreadEvents.Enqueue(resize);
            }

            bgfx.render_frame(MSecDelay);
        }

        while (bgfx.render_frame(MSecDelay) != bgfx.RenderFrame.NoContext)
        {
            
        }
        
        apiThread.Dispose();
        
        Glfw.Terminate();
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

    private static void* GlfwNativeWindowHandle(WindowHandle* windowHandle, out void* display)
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
                //window = (void*) nativeWindow.Cocoa!.Value;
                window = MetalWindowTest.SetupMetalLayer(nativeWindow.Cocoa!.Value);
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

    private static void GlfwDestroyWindow(WindowHandle* windowHandle)
    {
        if (windowHandle == null)
            return;

        Glfw.DestroyWindow(windowHandle);
    }
}