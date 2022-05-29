using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Index;

public unsafe class DynamicIndexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.DynamicIndexBufferHandle _dynamicIndexBufferHandle;

    public DynamicIndexBuffer(uint numberOfIndices, BufferFlags bufferFlags)
    {
        _dynamicIndexBufferHandle = bgfx.create_dynamic_index_buffer(numberOfIndices, (ushort)bufferFlags);
    }
    
    public DynamicIndexBuffer(Span<TDataType> data, BufferFlags bufferFlags)
    {
        var flags = (ushort)(IndexBufferUtils.IsInt32<TDataType>()
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.Index32
            : (ushort)bufferFlags);

        fixed (void* dataP = data)
        {
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));

            _dynamicIndexBufferHandle = bgfx.create_dynamic_index_buffer_mem(dataHandle, flags);
        }
    }

    public ushort Handle => _dynamicIndexBufferHandle.idx;

    public void Update(Span<TDataType> data, uint startIndex)
    {
        fixed (void* dataP = data)
        {
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));

            bgfx.update_dynamic_index_buffer(_dynamicIndexBufferHandle, startIndex, dataHandle);
        }
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