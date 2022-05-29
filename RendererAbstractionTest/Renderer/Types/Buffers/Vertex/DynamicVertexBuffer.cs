using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;
using RendererLibraries.BGFX;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Vertex;

public unsafe class DynamicVertexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.DynamicVertexBufferHandle _dynamicVertexBufferHandle;

    public DynamicVertexBuffer(uint numberOfVertices, VertexLayoutBuffer vertexLayoutBuffer,
        BufferFlags bufferFlags, bool resizeable = false)
    {
        var layout = vertexLayoutBuffer.VertexLayout;
        var flags = (ushort)(resizeable
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
            : (ushort)bufferFlags);

        _dynamicVertexBufferHandle = bgfx.create_dynamic_vertex_buffer(numberOfVertices, &layout, flags);
    }

    public DynamicVertexBuffer(Span<TDataType> data, VertexLayoutBuffer vertexLayoutBuffer,
        BufferFlags bufferFlags, bool resizeable = false)
    {
        fixed (void* dataP = data)
        {
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));
            var layout = vertexLayoutBuffer.VertexLayout;
            var flags = (ushort)(resizeable
                ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
                : (ushort)bufferFlags);

            _dynamicVertexBufferHandle = bgfx.create_dynamic_vertex_buffer_mem(dataHandle, &layout, flags);
        }
    }

    public ushort Handle => _dynamicVertexBufferHandle.idx;

    public void Update(Span<TDataType> data, uint startVertex)
    {
        fixed (void* dataP = data)
        {
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));

            bgfx.update_dynamic_vertex_buffer(_dynamicVertexBufferHandle, startVertex, dataHandle);
        }
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