using System.Runtime.InteropServices;
using Bgfx;

namespace BgfxRenderer.Utils;

public static unsafe class MemoryUtils
{
    public static bgfx.Memory* GetMemoryPointer(IntPtr data, uint size)
    {
        return bgfx.copy(data.ToPointer(), size);
    }
    
    public static bgfx.Memory* GetMemoryPointer<T>(T[] data) 
        where T : unmanaged
    {
        if (data is null || !data.Any())
            throw new ArgumentNullException(nameof(data));

        var gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
        var ptr = GetMemoryPointer(gcHandle.AddrOfPinnedObject(), (uint)(Marshal.SizeOf<T>() * data.Length));

        gcHandle.Free();
        
        return ptr;
    }
}