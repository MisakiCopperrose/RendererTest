using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Shaders;

public unsafe class ShaderUniform
{
    public ShaderUniform()
    {
    }

    public ShaderUniform(string name, UniformType type, ushort elementCount = 1)
    {
        Handle = bgfx.create_uniform(name, (bgfx.UniformType)type, elementCount);
        
        // TODO: log debug assert of handle
        var info = new bgfx.UniformInfo();
        
        bgfx.get_uniform_info(Handle, &info);

        Info = info;
    }

    public bgfx.UniformHandle Handle { get; init; }

    public bgfx.UniformInfo Info { get; init; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_uniform(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ShaderUniform()
    {
        ReleaseUnmanagedResources();
    }
}