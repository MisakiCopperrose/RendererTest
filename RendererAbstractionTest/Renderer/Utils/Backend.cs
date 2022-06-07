using Bgfx;
using RendererAbstractionTest.Renderer.Enums;
using RendererAbstractionTest.Renderer.Utils.Capabilities;
using RendererAbstractionTest.Renderer.Utils.Statistics;

namespace RendererAbstractionTest.Renderer.Utils;

public static class Backend
{
    private const int MaxNumberOfBackends = 10;

    public static SupportedApis GetCurrentBackend()
    {
        return TranslateRenderType(bgfx.get_renderer_type());
    }
    
    public static unsafe IReadOnlyCollection<SupportedApis> GetSupportedRenderers()
    {
        var array = new bgfx.RendererType[MaxNumberOfBackends];

        byte numOfBackends;
            
        fixed (bgfx.RendererType* pArray = array)
        {
            numOfBackends = bgfx.get_supported_renderers(MaxNumberOfBackends, pArray);
        }

        var supportedBackends = new List<SupportedApis>(numOfBackends);
        
        supportedBackends.AddRange(
            array
            .Select(TranslateRenderType)
            .Where(type => type != SupportedApis.NotSupported)
        );

        if (!supportedBackends.Any())
        {
            throw new Exception();
        }

        return supportedBackends;
    }
    
    public static SupportedApis TranslateRenderType(bgfx.RendererType rendererType)
    {
        switch (rendererType)
        {
            case bgfx.RendererType.Noop:
                break;
            case bgfx.RendererType.Agc:
                break;
            case bgfx.RendererType.Direct3D9:
                return SupportedApis.D3D9;
            case bgfx.RendererType.Direct3D11:
                break;
            case bgfx.RendererType.Direct3D12:
                return SupportedApis.D3D12;
            case bgfx.RendererType.Gnm:
                break;
            case bgfx.RendererType.Metal:
                return SupportedApis.Metal;
            case bgfx.RendererType.Nvn:
                break;
            case bgfx.RendererType.OpenGLES:
                break;
            case bgfx.RendererType.OpenGL:
                return SupportedApis.OpenGl;
            case bgfx.RendererType.Vulkan:
                return SupportedApis.Vulkan;
            case bgfx.RendererType.WebGPU:
                break;
            case bgfx.RendererType.Count:
                break;
            default:
                // TODO: Setup debug messaging
                throw new ArgumentOutOfRangeException();
        }

        return SupportedApis.NotSupported;
    }

    public static RendererStatistics GetStatistics()
    {
        throw new NotImplementedException();
    }
    
    public static BackendCapabilities GetCapabilities()
    {
        throw new NotImplementedException();
    }
}