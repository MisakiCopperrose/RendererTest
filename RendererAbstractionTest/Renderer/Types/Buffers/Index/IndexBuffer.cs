using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Index;

public unsafe class IndexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.IndexBufferHandle _indexBufferHandle;

    public IndexBuffer(Span<TDataType> data, BufferFlags bufferFlags)
    {
        var flags = (ushort)(IndexBufferUtils.IsInt32<TDataType>()
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.Index32
            : (ushort)bufferFlags);

        fixed (void* dataP = data)
        {
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));

            _indexBufferHandle = bgfx.create_index_buffer(dataHandle, flags);
        }
    }

    public ushort Handle => _indexBufferHandle.idx;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_index_buffer(_indexBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~IndexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}