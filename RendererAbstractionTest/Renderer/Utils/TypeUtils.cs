namespace RendererAbstractionTest.Renderer.Utils;

public static class TypeUtils
{
    public static bool IsInt32<TDataType>()
        where TDataType : unmanaged
    {
        return typeof(TDataType) == typeof(int) ||
               typeof(TDataType) == typeof(uint) ||
               typeof(TDataType) == typeof(float);
    }
}