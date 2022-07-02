using Bgfx;
using BgfxRenderer.Buffer.Vertex.Layout;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffer.Vertex;

public unsafe class DynamicVertexBuffer<TDataType> : IDisposable
    where TDataType : unmanaged
{
    public DynamicVertexBuffer(uint vertexCount, VertexLayout vertexLayout, BufferFlags bufferFlags = BufferFlags.None)
    {
        var layout = vertexLayout.Buffer;

        Handle = bgfx.create_dynamic_vertex_buffer(vertexCount, &layout, (ushort)bufferFlags);
    }

    public DynamicVertexBuffer(TDataType[] data, VertexLayout vertexLayout, BufferFlags bufferFlags = BufferFlags.None)
    {
        var layout = vertexLayout.Buffer;
        var handle = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_dynamic_vertex_buffer_mem(handle, &layout, (ushort)bufferFlags);
    }

    public bgfx.DynamicVertexBufferHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    public void Update(uint startVertex, TDataType[] data)
    {
        var handle = MemoryUtils.GetMemoryPointer(data);

        bgfx.update_dynamic_vertex_buffer(Handle, startVertex, handle);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_dynamic_vertex_buffer(Handle);
    }

    ~DynamicVertexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}