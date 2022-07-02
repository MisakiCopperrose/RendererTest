using System.Drawing;

namespace Renderer.Utils;

public static class ColorUtils
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static uint ColorToUintRgba(this Color color)
    {
        var argb = (uint)color.ToArgb();
        // Destruct argb value then reassemble to rgba value in correct order using bitwise operator
        return (argb & 0x00ff0000) << 8 | // Get red component uint value
               (argb & 0x0000ff00) << 8 | // Get green 
               (argb & 0x000000ff) << 8 | // Get blue
               (argb & 0xff000000) >> 24; // Get alpha
    }
}