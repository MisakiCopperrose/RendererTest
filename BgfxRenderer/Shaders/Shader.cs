using Bgfx;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Shaders;

public unsafe class Shader : IDisposable
{
    public Shader(string filename)
    {
        Name = filename;

        var path = $"{ShaderUtils.GetShaderPath()}\\{filename}.bin";

        if (!FileUtils.TryReadFile(path, out var data))
            return;

        Handle = bgfx.create_shader(MemoryUtils.GetMemoryPointer(data));

        // TODO: log debug assert shader handle valid

        Uniforms = ShaderUtils.GetShaderUniforms(Handle);
    }

    public string Name { get; }

    public bgfx.ShaderHandle Handle { get; }

    public IReadOnlyList<ShaderUniform> Uniforms { get; } = Array.Empty<ShaderUniform>();

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_shader(Handle);

        foreach (var uniform in Uniforms) uniform.Dispose();
    }

    ~Shader()
    {
        ReleaseUnmanagedResources();
    }
}