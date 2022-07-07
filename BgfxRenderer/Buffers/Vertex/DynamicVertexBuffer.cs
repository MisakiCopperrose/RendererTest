using Bgfx;
using BgfxRenderer.Buffers.Vertex.Layout;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffers.Vertex;

public unsafe class DynamicVertexBuffer<TDataType> : IDisposable
    where TDataType : unmanaged
{
    public DynamicVertexBuffer(uint vertexCount, VertexLayout vertexLayout, bool resizeable,
        BufferFlags bufferFlags = BufferFlags.None)
    {
        var layout = vertexLayout.Buffer;
        var flags = (ushort)(resizeable
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
            : (ushort)bufferFlags);

        Handle = bgfx.create_dynamic_vertex_buffer(vertexCount, &layout, flags);
    }

    public DynamicVertexBuffer(TDataType[] data, VertexLayout vertexLayout, bool resizeable,
        BufferFlags bufferFlags = BufferFlags.None)
    {
        var layout = vertexLayout.Buffer;
        var handle = MemoryUtils.GetMemoryPointer(data);
        var flags = (ushort)(resizeable
            ? (ushort)bufferFlags | (ushort)bgfx.BufferFlags.AllowResize
            : (ushort)bufferFlags);

        Handle = bgfx.create_dynamic_vertex_buffer_mem(handle, &layout, flags);
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
        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.update_dynamic_vertex_buffer(Handle, startVertex, memoryPointer);
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