//

using System.Text;

var house = new Builduing();

//ground floor 300
using(new BuildingContext(3000))
{
    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));


    // 1st floor 3500
    using (new BuildingContext(3500))
    {
        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
    }

    //2nd floor 3000
    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
}

Console.WriteLine(house.ToString());    

//height is an ambient context

public sealed class BuildingContext : IDisposable // - ambient context - not thread safe - find solution for this.
{
    public int WallHeight;
    private static Stack<BuildingContext> stack = new Stack<BuildingContext>();

    static BuildingContext()
    {
        stack.Push(new BuildingContext(0));
    }

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        stack.Push(this);

    }

    public static BuildingContext Current => stack.Peek();

    public void Dispose()
    {
        if (stack.Count > 1)
            stack.Pop();
    }
}

public class Builduing
{
    public List<Wall> Walls = new List<Wall>();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach(var wall in Walls)
        {
            sb.Append(wall.ToString());
        }
        return sb.ToString();
    }
}

public class Point
{
    private int x, y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";    
    }
}

public class Wall
{
    public Point Start, End;
    public int Height;

    public Wall(Point start, Point end)
    {
        Start = start;
        End = end;
        this.Height = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
        return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}, {nameof(Height)}: {Height}";
    }
}