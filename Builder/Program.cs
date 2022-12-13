var asd = new Creature("Goblin", 2, 2);
Console.WriteLine(asd);

var root = new CreatureModifier(asd);

Console.WriteLine("double attack");

root.Add(new DoubleAttackModifier(asd));

Console.WriteLine("Increase the defense");

root.Add(new IncreaseDefenseeModifier(asd));

root.Handle();

Console.WriteLine(asd);

public class Creature
{
    public string Name;
    public int Attack, Defense;

    public Creature(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }
}

public class NoBonusModifier: CreatureModifier
{
    public NoBonusModifier(Creature creature) : base(creature)
    {

    }

    public override void Handle()
    {
        //No base call, nobody is traversing the linked list;
    }
}

public class CreatureModifier
{
    protected Creature creature;
    protected CreatureModifier next; //linked list

    public CreatureModifier(Creature creature)
    {
        this.creature = creature;
    }

    public void Add(CreatureModifier creatureModifier)
    {
        if (next != null) { next.Add(creatureModifier); }
        else next = creatureModifier;
    }

    public virtual void Handle() => next?.Handle();
}

public class DoubleAttackModifier: CreatureModifier
{
    public DoubleAttackModifier(Creature creature) : base(creature)
    { }

    public override void Handle()
    {
        Console.WriteLine($"Doubling {creature.Name}'s attack");
        creature.Attack *= 2;
        base.Handle();
    }
}

public class IncreaseDefenseeModifier : CreatureModifier
{
    public IncreaseDefenseeModifier(Creature creature) : base (creature)
    {

    }

    public override void Handle()
    {
        Console.WriteLine($"Increasing {creature.Name}'s defense)");
        creature.Defense += 3;
        base.Handle(); 
    }
}