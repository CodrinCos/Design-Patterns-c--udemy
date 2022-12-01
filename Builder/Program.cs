var square = new Square(2.23f);

Console.WriteLine(square.AsString());

var redSquare = new ColoredShape(square, "red");

Console.WriteLine(redSquare.AsString());

var redhalftransparent = new TransparentShape(redSquare, 0.5f);

Console.WriteLine(redhalftransparent.AsString());

public class TransparentShape : IShape
{
    private IShape shape;
    private float transparency;

    public TransparentShape(IShape shape, float transparency)
    {
        this.shape = shape;
        this.transparency = transparency;
    }

    public string AsString() => $"{shape.AsString()} has {transparency * 100.0}% transparency";
}


public class ColoredShape: IShape
{
    private IShape shape;

    private string color;

    public ColoredShape(IShape shape, string color)
    {
        this.shape = shape;
        this.color = color;
    }

    public string AsString() => $"{shape.AsString()} has the color {color}";
}

public interface IShape
{
    string AsString();
}

public class Circle : IShape
{
    private float radius;

    public Circle(float radius)
    {
        this.radius = radius;
    }

    public string AsString() => $"A circle with radius {radius}";

    public void Resize(float factorOfResizing)
    {
        radius *= factorOfResizing;
    }
}

public class Square : IShape
{
    private float side;

    public Square(float side)
    {
        this.side = side;
    }

    public string AsString() => $"A square with side {side}";
}