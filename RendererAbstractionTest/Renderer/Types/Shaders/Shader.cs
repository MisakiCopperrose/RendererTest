using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Shaders;

public unsafe class Shader : IDisposable
{
    private readonly bgfx.ShaderHandle _shaderHandle;

    public Shader(string filename)
    {
        var stringPath = $"{GetShaderPath()}{filename}.bin";
        var fileBytes = File.ReadAllBytes(stringPath);

        fixed (void* pFileBytes = fileBytes)
        {
            var dataHandle = bgfx.make_ref(pFileBytes, (uint)(sizeof(byte) * fileBytes.Length));
            _shaderHandle =  bgfx.create_shader(dataHandle);
        }
    }

    public ushort Handle => _shaderHandle.idx;

    private string GetShaderPath()
    {
        switch (bgfx.get_renderer_type())
        {
            case bgfx.RendererType.Agc:
                break;
            case bgfx.RendererType.Direct3D9:
                return "shaders\\dx9\\";
            case bgfx.RendererType.Direct3D11:
                return "shaders\\dx11\\";
            case bgfx.RendererType.Direct3D12:
                return "shaders\\dx12\\";
            case bgfx.RendererType.Gnm:
                return "shaders\\pssl\\";
            case bgfx.RendererType.Metal:
                return "Shaders/metal/";
            case bgfx.RendererType.Nvn:
                break;
            case bgfx.RendererType.OpenGLES:
                return "shaders\\essl\\";
            case bgfx.RendererType.OpenGL:
                return "shaders\\glsl\\";
            case bgfx.RendererType.Vulkan:
                return "shaders\\spirv\\";
            case bgfx.RendererType.WebGPU:
                break;
            case bgfx.RendererType.Noop:
                break;
            case bgfx.RendererType.Count:
                break;
            default:
                return string.Empty;
        }
        
        return string.Empty;
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_shader(_shaderHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Shader()
    {
        ReleaseUnmanagedResources();
    }
}