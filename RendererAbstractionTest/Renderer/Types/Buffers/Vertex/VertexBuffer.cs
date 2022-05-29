using Bgfx;
using RendererAbstractionTest.Renderer.Types.Buffers.Vertex.Layout;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Vertex;

public unsafe class VertexBuffer<TDataType> : IBuffer
    where TDataType : unmanaged
{
    private readonly bgfx.VertexBufferHandle _vertexBufferHandle;

    public VertexBuffer(Span<TDataType> data, VertexLayoutBuffer vertexLayoutBuffer, BufferFlags bufferFlag)
    {
        fixed (void* dataP = data)
        {
            var vertexLayout = vertexLayoutBuffer.VertexLayout;
            var dataHandle = bgfx.make_ref(dataP, (uint)(sizeof(TDataType) * data.Length));

            _vertexBufferHandle = bgfx.create_vertex_buffer(
                dataHandle,
                &vertexLayout,
                (ushort)bufferFlag
            );
        }
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