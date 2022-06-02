using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Shaders;

public class ShaderProgram : IDisposable
{
    private readonly bgfx.ProgramHandle _programHandle;

    public ShaderProgram(Shader vertexShader, Shader fragmentShader, bool destroyAfterUse = false)
    {
        var vertexHandle = new bgfx.ShaderHandle()
        {
            idx = vertexShader.Handle
        };
        
        var fragmentHandle = new bgfx.ShaderHandle
        {
            idx = fragmentShader.Handle
        };

        // TODO: change destroy event
        _programHandle = bgfx.create_program(vertexHandle, fragmentHandle, true);
    }

    public ShaderProgram(Shader computeShader, bool destroyAfterUse = false)
    {
        var computeHandle = new bgfx.ShaderHandle()
        {
            idx = computeShader.Handle
        };

        _programHandle = bgfx.create_compute_program(computeHandle, false);
        
        if (!destroyAfterUse) 
            return;
        
        computeShader.Dispose();
    }
    
    public ushort Handle => _programHandle.idx;

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_program(_programHandle);
    }

    ~ShaderProgram()
    {
        ReleaseUnmanagedResources();
    }
}