using System.Runtime.InteropServices;
using Bgfx;

namespace RendererAbstractionTest.Renderer.Utils;

public static unsafe class MemoryUtils
{
    public static bgfx.Memory* Create(IntPtr data, uint size)
    {
        return bgfx.copy(data.ToPointer(), size);
    }

    public static bgfx.Memory* Create<T>(T[] data) 
        where T : struct
    {
        if (data == null || data.Length == 0)
            throw new ArgumentNullException(nameof(data));

        var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
        var ptr = Create(gcHandle.AddrOfPinnedObject(), (uint)(Marshal.SizeOf<T>() * data.Length));

        gcHandle.Free();
        
        return ptr;
    }
}