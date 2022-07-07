using Bgfx;

namespace BgfxRenderer.Buffers.Instance;

public struct InstanceBuffer
{
    public InstanceBuffer()
    {
        throw new NotImplementedException();
        
        Buffer = new bgfx.InstanceDataBuffer();
        
        // bgfx.alloc_instance_data_buffer();
    }
    
    public string Name { get; set; } = string.Empty;

    public bgfx.InstanceDataBuffer Buffer { get; }
}