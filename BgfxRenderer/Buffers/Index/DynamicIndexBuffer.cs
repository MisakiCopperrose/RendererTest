using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffers.Index;

public unsafe class DynamicIndexBuffer<TDataType> : IDisposable
    where TDataType: unmanaged
{
    public DynamicIndexBuffer(TDataType[] data, bool resizeable, BufferFlags bufferFlags = BufferFlags.None)
    {
        var flags = (ushort) (TypeUtils.IsInt32<TDataType>()
            ? (ushort) bufferFlags | (ushort) bgfx.BufferFlags.Index32
            : (ushort) bufferFlags);
        
        flags = (ushort) (resizeable
            ? flags | (ushort) bgfx.BufferFlags.AllowResize
            : flags);

        var dataBuffer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_dynamic_index_buffer_mem(dataBuffer, flags);
    }
    
    public DynamicIndexBuffer(uint indexCount, bool resizeable, BufferFlags bufferFlags = BufferFlags.None)
    {
        var flags = (ushort) (TypeUtils.IsInt32<TDataType>()
            ? (ushort) bufferFlags | (ushort) bgfx.BufferFlags.Index32
            : (ushort) bufferFlags);

        flags = (ushort) (resizeable
            ? flags | (ushort) bgfx.BufferFlags.AllowResize
            : flags);
        
        Handle = bgfx.create_dynamic_index_buffer(indexCount, flags);
    }
    
    public bgfx.DynamicIndexBufferHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    public void Update(TDataType[] data, uint startIndex)
    {
        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.update_dynamic_index_buffer(Handle, startIndex, memoryPointer);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_dynamic_index_buffer(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~DynamicIndexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}