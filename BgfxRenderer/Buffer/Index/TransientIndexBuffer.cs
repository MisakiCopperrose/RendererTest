using Bgfx;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Buffer.Index;

public unsafe class TransientIndexBuffer<TDataType>
    where TDataType : unmanaged
{
    public TransientIndexBuffer(TDataType[] data)
    {
        var isInt32 = TypeUtils.IsInt32<TDataType>();
        var buffer = new bgfx.TransientIndexBuffer();
        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.alloc_transient_index_buffer(&buffer, (uint)data.Length, isInt32);

        buffer.data = memoryPointer->data;

        Buffer = buffer;
    }

    public bgfx.TransientIndexBuffer Buffer { get; }

    public string Name { get; set; } = string.Empty;
}