using Bgfx;

namespace BgfxRenderer.Shaders;

public class ShaderProgram 
{
    public ShaderProgram(Shader vertex, Shader fragment, bool destroyShaders)
    {
        Handle = bgfx.create_program(vertex.Handle, fragment.Handle, false);

        // TODO: handle assertion and logging
        
        if (!destroyShaders) 
            return;
        
        vertex.Dispose();
        fragment.Dispose();
    }
    
    public ShaderProgram(Shader compute, bool destroyShaders)
    {
        Handle = bgfx.create_compute_program(compute.Handle, false);
        
        if (!destroyShaders) 
            return;
        
        compute.Dispose();
    }
    
    public bgfx.ProgramHandle Handle { get; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_program(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ShaderProgram()
    {
        ReleaseUnmanagedResources();
    }
}