using System.Numerics;
using RendererAbstractionTest.Renderer.Meshes;
using RendererAbstractionTest.Renderer.Structs;
using RendererAbstractionTest.Renderer.Types.Buffers;
using RendererAbstractionTest.Renderer.Types.Buffers.Index;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex;
using RendererAbstractionTest.Renderer.Types.Shaders;
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

    private static readonly Vector3 CameraPosition = new(0.0f, 0.0f, 5.0f);
    private static readonly Vector3 CameraTarget = Vector3.Zero;
    private static readonly Vector3 CameraDirection = Vector3.Normalize(CameraPosition - CameraTarget);
    private static readonly Vector3 CameraRight = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, CameraDirection));
    private static readonly Vector3 CameraUp = Vector3.Cross(CameraDirection, CameraRight);

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
            (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth),
            0x303030ff,
            1.0f,
            0
        );

        bgfx.set_view_rect(0, 0, 0, (ushort)_window.Width, (ushort)_window.Height);

        var vertexBuffer = new VertexBuffer<PosColor>(Cube.Vertices, PosColor.VertexLayoutBuffer, BufferFlags.None);
        var indexBuffer = new IndexBuffer<int>(Cube.Indices, BufferFlags.None);
        var vertexShader = new Shader("vs_cubes");
        var fragmentShader = new Shader("fs_cubes");
        var program = new ShaderProgram(vertexShader, fragmentShader);

        var showStats = false;
        var exit = false;

        while (!exit)
        {
            bgfx.touch(ClearView);

            var lookAt = Matrix4x4.CreateLookAt(CameraPosition, CameraTarget, CameraUp);

            var projection = Matrix4x4.CreatePerspectiveFieldOfView(
                90f * MathF.PI / 180f,
                800f / 600f,
                0.01f,
                1000.0f
            );

            bgfx.set_view_transform(0, &lookAt, &projection);

            var mtxX = Matrix4x4.CreateRotationX(20f);
            var mtxY = Matrix4x4.CreateRotationY(25f);

            var mtx = mtxX * mtxY;

            bgfx.set_transform(&mtx, 1);

            bgfx.set_vertex_buffer(
                0,
                new bgfx.VertexBufferHandle
                {
                    idx = vertexBuffer.Handle
                },
                0,
                (uint)Cube.Vertices.Length
            );

            bgfx.set_index_buffer(
                new bgfx.IndexBufferHandle
                {
                    idx = indexBuffer.Handle
                },
                0,
                (uint)Cube.Indices.Length
            );

            bgfx.submit(
                ClearView,
                new bgfx.ProgramHandle
                {
                    idx = program.Handle
                },
                0,
                (byte)bgfx.DiscardFlags.All
            );

            bgfx.dbg_text_clear(0, false);

            bgfx.set_debug((uint)(showStats ? bgfx.DebugFlags.Stats : bgfx.DebugFlags.Text));

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