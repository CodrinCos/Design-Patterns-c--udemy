
using MoreLinq;

var db = SingletonDatabase.Instance;
var city = "Tokyo";
Console.WriteLine($"{city} has population {db.GetPopulation(city)}");



public interface IDatabase
{
    int GetPopulation(string name);
}


public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> capitals;

    private SingletonDatabase()
    {
        Console.WriteLine("init db");

        capitals = File.ReadAllLines("Capitals.txt")
                    .Batch(2)
                    .ToDictionary(
                         list => list.ElementAt(0).Trim(), 
                         list => int.Parse(list.ElementAt(1))
                     );
    }
    public int GetPopulation(string name)
    {
        return capitals[name];
    }

    private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    public static SingletonDatabase Instance => instance.Value;
}