using System.Text;

var drawing = new GraphicObject { Name = "MyDrawing" };
drawing.Children.Add(new Square { Color = "Red"});
drawing.Children.Add(new Circle { Color = "Yellow"});

var group = new GraphicObject();
group.Children.Add(new Circle { Color = "Blue" });
group.Children.Add(new Square { Color = "Blue" });
drawing.Children.Add(group);

Console.WriteLine(drawing);

class GraphicObject
{
    public virtual string Name { get; set; } = "Group";
    public string Color;

    private Lazy<List<GraphicObject>> childreen = new Lazy<List<GraphicObject>>();
    public List<GraphicObject> Children => childreen.Value;

    private void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string('*', depth))
            .Append(string.IsNullOrEmpty(Color) ? string.Empty : Color)
            .AppendLine(Name);

        foreach(var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 0);
        return sb.ToString();
    }
}

class Circle : GraphicObject
{
    public override string Name => "Circle";
}

class Square : GraphicObject
{
    public override string Name => "Square";
}

