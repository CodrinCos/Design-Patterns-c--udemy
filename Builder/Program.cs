public class Bird
{
    public int Weight { get; set; }
    public void Fly()
    {
        Console.WriteLine("I am flying");
    }
}

public interface IBirth
{
    int Weight { get; set; }
    void Fly();
}

public class Lizard
{
    public int Weight { get; set; }
    public void Crawl()
    {
        Console.WriteLine("I am crawling");
    }
}

public interface ILizard
{
    int Weight { get; set; }
    void Crawl();
}

public class Dragon : ILizard, IBirth
{
    private Bird bird;
    private Lizard lizard;
    private int wight;

    public Dragon(Bird bird, Lizard lizard)
    {
        this.bird = bird ?? throw new ArgumentNullException(paramName: nameof(bird));
        this.lizard = lizard ?? throw new ArgumentNullException(paramName: nameof(lizard));
    }

    public int Weight { get => wight; set { wight = value; bird.Weight = value; lizard.Weight = value; } }

    public void Crawl()
    {
        lizard.Crawl();
    }

    public void Fly()
    {
        bird.Fly();
    }
}