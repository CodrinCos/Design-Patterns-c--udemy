
var c = new Creature();
c.Agility = 10; // c.set_Agility(10) - this will not happen
				// replacing this with brand new
				// c.Agility = new Property<int>(10)
c.Agility = 10;
public class Creature
{
	//public Property<int> Agility { get; set; } // this is not the right approach due to c# constraints and because we cannot overwrite = operator for setter.
	//the above has to be rewritten as:
	private Property<int> agility = new Property<int>();
	public int Agility
	{
		get => agility.Value;
		set => agility.Value = value;
		//this is not required in c++ because you can specify what should happen when "="
	}
}

public class Property<T> where T : new()
{
    private T value;

	public T Value
	{
		get => value;
		set
		{
			if (Equals(this.value, value)) return;
			Console.WriteLine($"Assigning value to {value}");
			this.value = value;
		}
	}

	//Creates an instance of T
	//this(default(T)) - this has null as default value for strings for example
	public Property() : this(Activator.CreateInstance<T>())
	{

	}

	public Property(T value)
	{
		this.value = value;
	}

	public static implicit operator T(Property<T> property)
	{
		return property.Value; // int n = p_int
	}

	public static implicit operator Property<T>(T value)
	{
		return new Property<T>(value); //Property<int> p = 123;
	}

	public bool Equals(Property<T> other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;

		return EqualityComparer<T>.Default.Equals(value, other.value);
	}

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;

		if (obj.GetType() != this.GetType()) return false;

		return Equals((Property<T>)obj);
    }

    public override int GetHashCode()
    {
		return value.GetHashCode();
    }
}