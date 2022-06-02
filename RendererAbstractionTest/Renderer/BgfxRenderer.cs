using System.Numerics;
using Bgfx;
using RendererAbstractionTest.Renderer.Meshes;
using RendererAbstractionTest.Renderer.Structs;
using RendererAbstractionTest.Renderer.Types.Buffers;
using RendererAbstractionTest.Renderer.Types.Buffers.Index;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex;
using RendererAbstractionTest.Renderer.Types.Shaders;
using RendererAbstractionTest.Window;

namespace RendererAbstractionTest.Renderer;

public unsafe class BgfxRenderer : IDisposable
{
    private readonly IWindow _window;
    private readonly Task _update;

    private VertexBuffer<PosColor> _vertexBuffer;
    private IndexBuffer<ushort> _indexBuffer;
    private ShaderProgram _shaderProgram;

    public BgfxRenderer(IWindow window)
    {
        _window = window;

        _update = new Task(Update);
    }

    public void Start()
    {
        _update.Start();
    }

    private void Update()
    {
        bgfx.render_frame(8);

        var pd = new bgfx.PlatformData
        {
            nwh = _window.NativeWindowHandle(out var display)
        };

        bgfx.set_platform_data(&pd);

        var init = new bgfx.Init();

        bgfx.init_ctor(&init);

        init.type = bgfx.RendererType.Direct3D12;
        init.resolution = new bgfx.Resolution
        {
            format = bgfx.TextureFormat.RGBA8, // Format needed for d3d11/12 backend
            width = (uint) _window.Width,
            height = (uint) _window.Height,
            reset = (uint) bgfx.ResetFlags.Vsync | (uint) bgfx.ResetFlags.FlushAfterRender
        };

        bgfx.init(&init);

        _vertexBuffer =
            new VertexBuffer<PosColor>(Cube.Vertices, PosColor.VertexLayoutBuffer, BufferFlags.None);
        _indexBuffer =
            new IndexBuffer<ushort>(Cube.Indices, BufferFlags.None);

        var vertexShader = new Shader("vs_cubes");
        var fragmentShader = new Shader("fs_cubes");

        _shaderProgram = new ShaderProgram(vertexShader, fragmentShader);

        bgfx.reset((uint) _window.Width, (uint) _window.Height,
            (uint) bgfx.ResetFlags.Vsync | (uint) bgfx.ResetFlags.FlushAfterRender, bgfx.TextureFormat.Count);

        bgfx.set_debug((uint) (bgfx.DebugFlags.Text | bgfx.DebugFlags.Stats));


        while (!_window.WindowShouldClose)
        {
            bgfx.set_view_clear(0, (ushort) (bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth), 0x443355FF, 1, 0);
            bgfx.set_view_rect(0, 0, 0, (ushort) _window.Width, (ushort) _window.Height);

            var viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -35.0f), Vector3.Zero, Vector3.UnitY);
            var projMatrix = Matrix4x4.CreatePerspectiveFieldOfView((float) Math.PI / 3,
                (float) _window.Width / _window.Height, 0.1f, 100.0f);

            bgfx.set_view_transform(0, &viewMatrix.M11, &projMatrix.M11);

            bgfx.touch(0);

            bgfx.set_vertex_buffer(0, new bgfx.VertexBufferHandle
                {
                    idx = _vertexBuffer.Handle
                },
                0, (uint) Cube.Vertices.Length);

            bgfx.set_index_buffer(new bgfx.IndexBufferHandle
                {
                    idx = _indexBuffer.Handle
                },
                0, (uint) Cube.Indices.Length);

            bgfx.set_state((ulong) bgfx.StateFlags.Default, 0);

            bgfx.submit(0, new bgfx.ProgramHandle
            {
                idx = _shaderProgram.Handle
            }, 0, 0);

            bgfx.frame(false);
        }
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