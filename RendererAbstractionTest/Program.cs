using Bgfx;
using RendererAbstractionTest.Renderer;
using RendererAbstractionTest.Window;

var factory = new TaskFactory();
var window = new GlfwWindow();
var renderer = new BgfxRenderer(window);

window.Initialize();
factory.StartNew(renderer.Update);
window.PollEvents();