using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffers.Index;

public unsafe class IndexBuffer<TDataType> : IDisposable
    where TDataType : unmanaged
{
    private string _name = string.Empty;
    
    public IndexBuffer(TDataType[] data, BufferFlags bufferFlags)
    {
        var flags = (ushort) (TypeUtils.IsInt32<TDataType>()
            ? (ushort) bufferFlags | (ushort) bgfx.BufferFlags.Index32
            : (ushort) bufferFlags);

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_index_buffer(memoryPointer, flags);
    }

    public bgfx.IndexBufferHandle Handle { get; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_index_buffer_name(Handle, value, value.Length);

            _name = value;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_index_buffer(Handle);
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