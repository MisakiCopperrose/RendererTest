namespace RendererAbstractionTest.Renderer.Types.Buffers;

public interface IBuffer : IDisposable
{
    ushort Handle { get; }
}