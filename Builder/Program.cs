//constructed over a primitive type
//stronger typing


//50% = 0.5 100 

//We want to achieve:
using System.Diagnostics;

Console.WriteLine(10f * 5.Percent());
Console.WriteLine(2.Percent() + 5.Percent());


public static class PercentageExtensions
{
	public static Percentage Percent(this int value)
	{
		return new Percentage(value/100.0f);
	}

    public static Percentage Percent(this float value)
    {
        return new Percentage(value / 100.0f);
    }
}

[DebuggerDisplay("{value*100.0f}%")]
public struct Percentage
{
    private readonly float value;

	public Percentage(float value)
	{
		this.value = value;
	}

	//all the operators needs to be added if needed
	public static float operator *(float f, Percentage p)
	{
		return f * p.value;
	}

	public static Percentage operator +(Percentage a, Percentage b)
	{
		return new Percentage(a.value + b.value);
	}

    public override string ToString()
    {
		return $"{value * 100}%";
    }

    public override bool Equals(object? obj)
    {
        return obj is Percentage percentage &&
               value == percentage.value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(value);
    }
}