using Bgfx;
using Renderer.Enums;
using Renderer.Utils;

namespace Renderer.Types.Buffers.Index;

public unsafe class DynamicIndexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.DynamicIndexBufferHandle _dynamicIndexBufferHandle;

    public DynamicIndexBuffer(uint numberOfIndices, BufferFlags bufferFlags)
    {
        _dynamicIndexBufferHandle = bgfx.create_dynamic_index_buffer(numberOfIndices, (ushort)bufferFlags);
    }

    public DynamicIndexBuffer(TDataType[] data, BufferFlags bufferFlags)
    {
        var dataHandle = MemoryUtils.Create(data);
        var flags = (ushort)(TypeUtils.IsInt32<TDataType>()
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.Index32
            : (ushort)bufferFlags);

        _dynamicIndexBufferHandle = bgfx.create_dynamic_index_buffer_mem(dataHandle, flags);
    }

    public ushort Handle => _dynamicIndexBufferHandle.idx;

    public void Update(TDataType[] data, uint startIndex)
    {
        var dataHandle = MemoryUtils.Create(data);

        bgfx.update_dynamic_index_buffer(_dynamicIndexBufferHandle, startIndex, dataHandle);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_dynamic_index_buffer(_dynamicIndexBufferHandle);
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