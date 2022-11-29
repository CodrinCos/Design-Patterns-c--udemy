
Dragon d = new Dragon { Age = 5 };

//to be able to access the crawl the dragon needs to be casted ((ILizard)d).Crawl()
//or
if (d is IBird bird)
{
    bird.Fly();
}

if (d is ILizard lizard)
{
    lizard.Crawl();
}



public interface ICreature
{
    int Age { get; set;}
}

public interface IBird: ICreature
{
    void Fly()
    {
        if (Age >= 10) { Console.WriteLine("I am flying!"); }
    }
}

public interface ILizard: ICreature
{
    void Crawl() 
    {
        if (Age < 10) Console.WriteLine("I am crawling");
    }
}
public class Organism { }
public class Dragon : Organism, ICreature, IBird, ILizard //ICreature can be deleted, but does not matter
{
    public int Age { get; set; }
}

//No option to inherit from dragon
//SmartDragon(Dragon) - wrap the dragon
//extension method - for extra implementation
//C#8 default interface methods


