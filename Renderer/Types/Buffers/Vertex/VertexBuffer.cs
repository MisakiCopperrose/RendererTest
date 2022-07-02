using Bgfx;
using Renderer.Enums;
using Renderer.Types.Buffers.Vertex.Layout;
using Renderer.Utils;

namespace Renderer.Types.Buffers.Vertex;

public unsafe class VertexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.VertexBufferHandle _vertexBufferHandle;

    public VertexBuffer(TDataType[] data, VertexLayoutBuffer vertexLayoutBuffer, BufferFlags bufferFlag)
    {
        var vertexLayout = vertexLayoutBuffer.VertexLayout;
        var dataBuffer = MemoryUtils.Create(data);

        _vertexBufferHandle = bgfx.create_vertex_buffer(dataBuffer, &vertexLayout, (ushort) bufferFlag);
    }

    public ushort Handle => _vertexBufferHandle.idx;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_vertex_buffer(_vertexBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~VertexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}