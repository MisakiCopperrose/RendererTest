using Bgfx;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;
using RendererAbstractionTest.Renderer.Utils;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Vertex;

public unsafe class DynamicVertexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.DynamicVertexBufferHandle _dynamicVertexBufferHandle;

    public DynamicVertexBuffer(uint numberOfVertices, VertexLayoutBuffer vertexLayoutBuffer,
        BufferFlags bufferFlags, bool resizeable = false)
    {
        var flags = (ushort)(resizeable
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
            : (ushort)bufferFlags);
        var vertexLayout = vertexLayoutBuffer.VertexLayout;

        _dynamicVertexBufferHandle = bgfx.create_dynamic_vertex_buffer(
            numberOfVertices,
            &vertexLayout,
            flags
        );
    }

    public DynamicVertexBuffer(TDataType[] data, VertexLayoutBuffer vertexLayoutBuffer,
        BufferFlags bufferFlags, bool resizeable = false)
    {
        var dataHandle = MemoryUtils.Create(data);
        var vertexLayout = vertexLayoutBuffer.VertexLayout;
        var flags = (ushort)(resizeable
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
            : (ushort)bufferFlags);

        _dynamicVertexBufferHandle = bgfx.create_dynamic_vertex_buffer_mem(
            dataHandle,
            &vertexLayout,
            flags
        );
    }

    public ushort Handle => _dynamicVertexBufferHandle.idx;

    public void Update(TDataType[] data, uint startVertex)
    {
        var dataHandle = MemoryUtils.Create(data);

        bgfx.update_dynamic_vertex_buffer(_dynamicVertexBufferHandle, startVertex, dataHandle);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_dynamic_vertex_buffer(_dynamicVertexBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~DynamicVertexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}