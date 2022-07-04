using Bgfx;
using BgfxRenderer.Buffer.Vertex.Layout;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffer.Vertex;

public unsafe class VertexBuffer<TDataType> : IDisposable
    where TDataType : unmanaged
{
    private string _name = string.Empty;

    public VertexBuffer(TDataType[] data, VertexLayout vertexLayout, BufferFlags bufferFlag)
    {
        var layout = vertexLayout.Buffer;
        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_vertex_buffer(memoryPointer, &layout, (ushort)bufferFlag);
    }

    public bgfx.VertexBufferHandle Handle { get; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_vertex_buffer_name(Handle, value, value.Length);

            _name = value;
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_vertex_buffer(Handle);
    }

    ~VertexBuffer()
    {
        ReleaseUnmanagedResources();
    }
}