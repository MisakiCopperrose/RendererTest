namespace Maths;

public struct Rectangle<T>
{
    public Rectangle(T x, T y, T width, T height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public T X { get; set; } 
    
    public T Y { get; set; }
    
    public T Width { get; set; }
    
    public T Height { get; set; }
}