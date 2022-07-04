using Bgfx;
using BgfxRenderer.Buffer.Vertex.Layout;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffer.Vertex;

public unsafe class TransientVertexBuffer<TDataType>
    where TDataType : unmanaged
{
    public TransientVertexBuffer(TDataType[] data, VertexLayout vertexLayout)
    {
        var layout = vertexLayout.Buffer;
        var buffer = new bgfx.TransientVertexBuffer();
        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.alloc_transient_vertex_buffer(&buffer, (uint)data.Length, &layout);

        buffer.data = memoryPointer->data;

        Buffer = buffer;
    }

    public bgfx.TransientVertexBuffer Buffer { get; }

    public string Name { get; set; } = string.Empty;
}