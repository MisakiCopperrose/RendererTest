using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Resources.Shaders;

public static unsafe class ShaderUtils
{
    private const int MaxShaderUniforms = 20;

    public static string GetShaderPath()
    {
        var folder = RuntimeData.CurrentConfigData.ShaderFolderPath;

        switch (RuntimeData.CurrentBackend)
        {
            case SupportedApis.Vulkan:
                return $"{folder}\\Vulkan";
            case SupportedApis.Metal:
                return $"{folder}\\Metal";
            case SupportedApis.D3D9:
                return $"{folder}\\D3D9";
            case SupportedApis.D3D12:
                return $"{folder}\\D3D12";
            case SupportedApis.OpenGl:
                return $"{folder}\\OpenGL";
            case SupportedApis.NotSupported:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static ShaderUniform[] GetShaderUniforms(bgfx.ShaderHandle shaderHandle)
    {
        var array = new bgfx.UniformHandle[MaxShaderUniforms];

        int count;

        fixed (bgfx.UniformHandle* arrayP = array)
        {
            count = bgfx.get_shader_uniforms(shaderHandle, arrayP, MaxShaderUniforms);
        }

        var uniformArray = new ShaderUniform[count];

        for (var i = 0; i < count; i++)
        {
            var handle = array[i];
            var info = new bgfx.UniformInfo();

            bgfx.get_uniform_info(handle, &info);

            uniformArray[i] = new ShaderUniform
            {
                Handle = handle,
                Info = info
            };
        }

        return uniformArray;
    }
}