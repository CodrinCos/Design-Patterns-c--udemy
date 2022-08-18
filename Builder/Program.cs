var person = new PersonBuilder()
    .Called("Cod")
    //And because of the extension now it can work too
    .WorkAs("Developer")
    .Build();

Console.WriteLine(person.Position);


public class Person
{
    public string Name, Position;
}

public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    //list of mutating function which will change the person
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    public TSelf Called(string name) => Do(p => p.Name = name);

    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

    public TSelf Do(Action<Person> action) => AddAction(action);

    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p => { action(p); return p; });
        return (TSelf)this;
    }
}

public sealed class PersonBuilder
    : FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
}

//Now instead of this, we can make more generic, look up
/*public sealed class PersonBuilder
{
    //list of mutating function which will change the person
    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

    public PersonBuilder Called(string name) => Do(p => p.Name = name);

    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

    public PersonBuilder Do(Action<Person> action) => AddAction(action);

    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p => { action(p); return p; });
        return this;
    }
}*/

//Now because we do not want to use inheritance, we will make use of extensions (follow open closed principle)
public static class PersonBuilderExtensions
{
    public static PersonBuilder WorkAs(this PersonBuilder builder, string position) 
        => builder.Do(p => p.Position = position);
}