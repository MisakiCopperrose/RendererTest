using Bgfx;
using Silk.NET.GLFW;

namespace RendererTest;

internal static unsafe class Program
{
    private static WindowHandle* _windowHandle;

    internal static void Main()
    {
        EntryGlfw.CurrentGlfw.SetErrorCallback(ErrorCallback);

        if (!EntryGlfw.CurrentGlfw.Init())
        {
            throw new GlfwException("GLFW INITIALIZATION FAILED!");
        }

        EntryGlfw.CurrentGlfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.NoApi);

        _windowHandle = EntryGlfw.CurrentGlfw.CreateWindow(800, 600, "BGFX Test Window", null, null);

        if (_windowHandle is null)
        {
            EntryGlfw.CurrentGlfw.Terminate();

            throw new GlfwException("GLFW WINDOW CREATION FAILED!");
        }
        
        var test = new Thread(Test);
        
        test.Start();
    }

    private static void Test()
    {
        var init = new bgfx.Init
        {
            type = bgfx.RendererType.Count,
            resolution = new bgfx.Resolution
            {
                format = bgfx.TextureFormat.BC1,
                width = 800,
                height = 600,
                reset = (uint)bgfx.ResetFlags.Vsync,
            },
        };

        EntryGlfw.GlfwSetWindow(_windowHandle);

        bgfx.init(&init);

        bgfx.set_debug((uint)bgfx.DebugFlags.Text);

        bgfx.set_view_clear(0,
            (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x303030ff,
            1.0f,
            0
        );

        while (!EntryGlfw.CurrentGlfw.WindowShouldClose(_windowHandle))
        {
            bgfx.set_view_rect(0, 0, 0, 800, 600);

            bgfx.touch(0);

            bgfx.frame(false);

            EntryGlfw.CurrentGlfw.PollEvents();
        }
        
        EntryGlfw.GlfwDestroyWindow(_windowHandle);

        EntryGlfw.CurrentGlfw.Terminate();
    }

    private static void ErrorCallback(ErrorCode error, string description)
    {
        throw new GlfwException($"GLFW ERROR: {error}, {description}");
    }
}