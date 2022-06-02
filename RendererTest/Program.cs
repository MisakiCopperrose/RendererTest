using System.Diagnostics;
using System.Numerics;
using Bgfx;
using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;

namespace RendererTest;

internal static unsafe class Program
{
    private static readonly Glfw Glfw = Glfw.GetApi();

    private static WindowHandle* _windowHandle;

    private static int _counter;

    private static readonly Vector3 CameraPosition = new(0.0f, 0.0f, 5.0f);
    private static readonly Vector3 CameraTarget = Vector3.Zero;
    private static readonly Vector3 CameraDirection = Vector3.Normalize(CameraPosition - CameraTarget);
    private static readonly Vector3 CameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, CameraDirection));
    private static readonly Vector3 CameraUp = Vector3.Cross(CameraDirection, CameraRight);

    private struct PosColorVertex
    {
        public PosColorVertex()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Abgr = 0;
        }

        public static bgfx.VertexLayout* Layout
        {
            get
            {
                var vertexLayout = new bgfx.VertexLayout();

                bgfx.vertex_layout_begin(&vertexLayout, bgfx.get_renderer_type());
                bgfx.vertex_layout_add(&vertexLayout, bgfx.Attrib.Position, 3, bgfx.AttribType.Float, false, false);
                bgfx.vertex_layout_add(&vertexLayout, bgfx.Attrib.Color0, 4, bgfx.AttribType.Uint8, true, false);
                bgfx.vertex_layout_end(&vertexLayout);

                return &vertexLayout;
            }
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public uint Abgr { get; set; }
    }

    private static readonly PosColorVertex[] CubeVertices =
    {
        new()
        {
            X = -1.0f, Y = 1.0f, Z = 1.0f, Abgr = 0xff000000
        },
        new()
        {
            X = 1.0f, Y = 1.0f, Z = 1.0f, Abgr = 0xff0000ff
        },
        new()
        {
            X = -1.0f, Y = -1.0f, Z = 1.0f, Abgr = 0xff00ff00
        },
        new()
        {
            X = 1.0f, Y = -1.0f, Z = 1.0f, Abgr = 0xff00ffff
        },
        new()
        {
            X = -1.0f, Y = 1.0f, Z = -1.0f, Abgr = 0xffff0000
        },
        new()
        {
            X = 1.0f, Y = 1.0f, Z = -1.0f, Abgr = 0xffff00ff
        },
        new()
        {
            X = -1.0f, Y = -1.0f, Z = -1.0f, Abgr = 0xffffff00
        },
        new()
        {
            X = 1.0f, Y = -1.0f, Z = -1.0f, Abgr = 0xffffffff
        }
    };

    private static readonly uint[] CubeTriList =
    {
        0, 1, 2, // 0
        1, 3, 2,
        4, 6, 5, // 2
        5, 6, 7,
        0, 2, 4, // 4
        4, 2, 6,
        1, 5, 3, // 6
        5, 7, 3,
        0, 4, 1, // 8
        4, 5, 1,
        2, 3, 6, // 10
        6, 3, 7,
    };

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

        init.type = bgfx.RendererType.Vulkan;
        init.capabilities = ulong.MaxValue;
        init.resolution = new bgfx.Resolution
        {
            width = 800,
            height = 600,
            reset = (uint)bgfx.ResetFlags.None
        };

        bgfx.init(&init);

        bgfx.set_debug((uint)bgfx.DebugFlags.Stats | (uint) bgfx.DebugFlags.Text);

        bgfx.set_view_clear(0,
            (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x443355FF,
            1.0f,
            0
        );

        bgfx.set_view_rect(0, 0, 0, 800, 600);

        while (!Glfw.WindowShouldClose(_windowHandle))
        {
            bgfx.touch(0);

            bgfx.frame(false);

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
                window = (void*)nativeWindow.Win32!.Value.Hwnd;
                break;
            case NativeWindowFlags.X11:
                display = (void*)nativeWindow.X11!.Value.Display;
                window = (void*)nativeWindow.X11!.Value.Window;
                break;
            case NativeWindowFlags.DirectFB:
                break;
            case NativeWindowFlags.Cocoa:
                window = (void*) nativeWindow.Cocoa!.Value;
                //window = MetalWindowTest.SetupMetalLayer(nativeWindow.Cocoa!.Value);
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

    private static void GlfwDestroyWindow(WindowHandle* windowHandle)
    {
        if (windowHandle == null)
            return;

        Glfw.DestroyWindow(windowHandle);
    }
}