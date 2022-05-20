using RendererAbstractionTest.Renderer;
using RendererAbstractionTest.Window;

var window = new GlfwWindow();

var renderer = new BgfxRenderer(window);

renderer.Init();

window.Start();