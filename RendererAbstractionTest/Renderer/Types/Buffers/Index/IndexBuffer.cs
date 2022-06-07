using Bgfx;
using RendererAbstractionTest.Renderer.Utils;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Index;

public unsafe class IndexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.IndexBufferHandle _indexBufferHandle;

    public IndexBuffer(TDataType[] data, BufferFlags bufferFlags)
    {
        var flags = (ushort) (TypeUtils.IsInt32<TDataType>()
            ? (ushort) bufferFlags | (ushort) bgfx.BufferFlags.Index32
            : (ushort) bufferFlags);

        var dataBuffer = MemoryUtils.Create(data);

        _indexBufferHandle = bgfx.create_index_buffer(dataBuffer, flags);
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