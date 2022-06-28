using System.Drawing;
using System.Numerics;
using Bgfx;
using RendererAbstractionTest.Renderer.Meshes;
using RendererAbstractionTest.Renderer.Structs;
using RendererAbstractionTest.Renderer.Types.Buffers;
using RendererAbstractionTest.Renderer.Types.Buffers.Index;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex;
using RendererAbstractionTest.Renderer.Types.Shaders;
using RendererAbstractionTest.Renderer.Types.Textures;
using RendererAbstractionTest.Renderer.Types.Views;
using RendererAbstractionTest.Window;

namespace RendererAbstractionTest.Renderer;

public unsafe class BgfxRenderer : IDisposable
{
    private readonly IWindow _window;

    public BgfxRenderer(IWindow window)
    {
        BgfxNative.SetBgfx();
        
        _window = window;
    }

    public void Update()
    {
        bgfx.render_frame(8);

        var pd = new bgfx.PlatformData
        {
            nwh = _window.NativeWindowHandle(out var display),
            ndt = display
        };

        bgfx.set_platform_data(&pd);

        var init = new bgfx.Init();

        bgfx.init_ctor(&init);

        init.type = bgfx.RendererType.Count;
        init.resolution = new bgfx.Resolution
        {
            format = bgfx.TextureFormat.RGBA16, // RGBA8 format needed for d3d11/12 backend
            width = (uint)_window.Width,
            height = (uint)_window.Height,
            reset = (uint)bgfx.ResetFlags.Vsync | (uint)bgfx.ResetFlags.FlushAfterRender
        };

        bgfx.init(&init);

        var vertexBuffer = new VertexBuffer<PosColor>(
            CubeBgfx.Vertices.Reverse().ToArray(),
            PosColor.VertexLayoutBuffer,
            BufferFlags.None
        );
        var indexBuffer = new IndexBuffer<ushort>(CubeBgfx.Indices, BufferFlags.None);
        var vertexShader = new Shader("vs_cubes");
        var fragmentShader = new Shader("fs_cubes");
        var shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
        var testTexture = Texture.CreateTexture2DFromFile("Textures/texture.png");
        var viewPass = new ViewPass();

        bgfx.reset(
            (uint)_window.Width,
            (uint)_window.Height,
            (uint)bgfx.ResetFlags.Vsync | (uint)bgfx.ResetFlags.FlushAfterRender,
            bgfx.TextureFormat.Count
        );

        bgfx.set_debug((uint)(bgfx.DebugFlags.Text | bgfx.DebugFlags.Stats));

        var counter = 0;

        while (!_window.WindowShouldClose)
        {
            counter++;

            viewPass.ViewRectangle = new Rectangle(0, 0, _window.Width, _window.Height);
            viewPass.ViewMatrix = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -5.0f), Vector3.Zero, Vector3.UnitY);
            viewPass.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 3f,
                (float)_window.Width / _window.Height, 0.1f, 100.0f);

            bgfx.touch(viewPass.Id);

            var rX = Matrix4x4.CreateRotationX(SineWave(counter));
            var rY = Matrix4x4.CreateRotationY(SineWave(counter));

            var rXy = rX * rY;

            bgfx.set_transform(&rXy, 1);

            bgfx.set_vertex_buffer(0, new bgfx.VertexBufferHandle
                {
                    idx = vertexBuffer.Handle
                },
                0, (uint)CubeBgfx.Vertices.Length);

            bgfx.set_index_buffer(new bgfx.IndexBufferHandle
                {
                    idx = indexBuffer.Handle
                },
                0, (uint)CubeBgfx.Indices.Length);

            bgfx.set_state((ulong)bgfx.StateFlags.Default, 0);

            bgfx.submit(0, new bgfx.ProgramHandle
            {
                idx = shaderProgram.Handle
            }, 0, 0);

            bgfx.frame(false);
        }
    }

    private float SineWave(float input, float amplitude = 10f, float frequency = 0.0001f)
    {
        return amplitude * MathF.Sin(2f * MathF.PI * frequency * input);
    }

    private static void ReleaseUnmanagedResources()
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