using Bgfx;

namespace RendererAbstractionTest.Renderer.Types.Buffers;

public enum BufferFlags
{
    None = bgfx.BufferFlags.None,
    ComputeRead = bgfx.BufferFlags.ComputeRead,
    ComputeWrite = bgfx.BufferFlags.ComputeWrite,
    ComputeReadWrite = bgfx.BufferFlags.ComputeReadWrite,
}