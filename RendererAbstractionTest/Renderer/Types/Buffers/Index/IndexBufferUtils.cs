namespace RendererAbstractionTest.Renderer.Types.Buffers.Index;

public static class IndexBufferUtils
{
    public static bool IsInt32<TDataType>()
        where TDataType : unmanaged
    {
        return typeof(TDataType) == typeof(int) ||
               typeof(TDataType) == typeof(uint) ||
               typeof(TDataType) == typeof(float);
    }
}