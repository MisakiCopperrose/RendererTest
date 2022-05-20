using RendererAbstractionTest.Window;
using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer;

public unsafe class BgfxRenderer : IDisposable
{
    private const int ClearView = 0;
    
    private bgfx.Init _init;
    private bgfx.Resolution _resolution;
    private bgfx.PlatformData _platformData;
    
    private readonly TaskFactory _initTasks;
    private readonly IWindow _window;

    public BgfxRenderer(IWindow window)
    {
        _window = window;
        _initTasks = new TaskFactory();

        _window.RenderFrame += () => RenderFrame();
    }

    public void Init()
    {
        RenderFrame();

        _initTasks.StartNew(InitRendererTask);

        while (RenderFrame() != bgfx.RenderFrame.NoContext)
        {
            
        }
    }

    private void InitRendererTask()
    {
        _resolution = new bgfx.Resolution
        {
            format = bgfx.TextureFormat.BC1,
            width = (uint)_window.Width,
            height = (uint)_window.Height,
            reset = (uint)bgfx.ResetFlags.Vsync,
            numBackBuffers = default,
            maxFrameLatency = default
        };

        _platformData = new bgfx.PlatformData
        {
            ndt = _window.DisplayHandle,
            nwh = _window.WindowHandle,
            context = default,
            backBuffer = default,
            backBufferDS = default
        };

        _init = new bgfx.Init
        {
            limits = default,
            type = bgfx.RendererType.Count,
            vendorId = (ushort)bgfx.PciIdFlags.None,
            deviceId = 0,
            capabilities = 0,
            debug = 0,
            profile = 0,
            platformData = _platformData,
            resolution = _resolution,
            callback = default,
            allocator = default
        };

        var init = _init;

        bgfx.init(&init);

        bgfx.set_view_clear(
            ClearView,
            (ushort) (bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x303030ff,
            1.0f,
            0
        );

        bgfx.set_view_rect_ratio(ClearView, 0, 0, bgfx.BackbufferRatio.Equal);
        
        var showStats = false;
        var exit = false;

        while (!exit)
        {
            bgfx.touch(ClearView);

            bgfx.dbg_text_clear(0, false);

            bgfx.set_debug((uint) (showStats ? bgfx.DebugFlags.Stats : bgfx.DebugFlags.Text));

            bgfx.frame(false);
        }
    }

    private bgfx.RenderFrame RenderFrame()
    {
        return bgfx.render_frame(16);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.shutdown();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        
        GC.SuppressFinalize(this);
    }

    ~BgfxRenderer()
    {
        ReleaseUnmanagedResources();
    }
}