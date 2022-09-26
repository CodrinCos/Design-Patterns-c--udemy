//Generic value adapter
//implement vector, 2 dimen, 3 dimen (int floating etc...)
//generic approach to implement all of these
//result => Vector2f, Vector3i, ...

//adapt a literal value to a type

var v = new Vector2i(1, 2);

v[0] = 0;

var vv = new Vector2i(2, 3);

//I want to do the following:
var result = v + vv;
//atm I cannot do it in Vector class because its a generic type



public interface IInteger
{
    int Value { get; }
}
public static class Dimensions
{ 
    public class Two : IInteger
    {
        public int Value => 2;
    }

    public class Three : IInteger
    {
        public int Value => 3;
    }
}

public class VectorOfFloat<TSelf, D> : 
    Vector<TSelf, float, D>
    where D : IInteger, new()
    where TSelf : Vector<TSelf, float, D>, new()
{

}

public class VectorOfInt<D> : Vector<VectorOfInt<D>, int, D>
    where D: IInteger, new()
{
    public VectorOfInt()
    {
    }

    public VectorOfInt(params int[] values) : base(values)
    {
    }

    public static VectorOfInt<D> operator +
        (VectorOfInt<D> left, VectorOfInt<D> right)
    {
        var result = new VectorOfInt<D>();
        var dim = new D().Value;
        for(int i = 0; i<dim; i++)
        {
            result[i] = left[i] + right[i];
        }

        return result;
    }
}

public class Vector2i: VectorOfInt<Dimensions.Two>
{
    public Vector2i()
    {
    }

    public Vector2i(params int[] values) : base(values)
    {
    }
}

public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
{

}

public class Vector<TSelf, T, D>
    where D : IInteger, new()
    where TSelf : Vector<TSelf, T, D>, new()
{
    public T[] data;

    public Vector()
    {
        data = new T[new D().Value];
    }

    //can't have a constructor because we do not know how many parameters it has => solution is to use params
    public Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        data = new T[requiredSize];

        var providedSize = values.Length;

        for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            data[i] = values[i];
    }

    //need a params factory method
    public static TSelf Create(params T[] values)
    {
        //return new Vector<T,D>(values); -> bad approach, becasue the result of this constructor will nopt be Vector3f it will be Vector<float, Dimensions.Three>
        //Solution is recursive generics, when you inherit from vectorfloat  which inherit from vector<float, D> you have to propagate additional info up to the base class

        var result = new TSelf();
        var requiredSize = new D().Value;
        result.data = new T[requiredSize];

        var providedSize = values.Length;

        for (int i = 0; i < Math.Min(requiredSize, providedSize); ++i)
            result.data[i] = values[i];

        return result;
    }

    public T this[int index]
    {
        get => data[index];
        set => data[index] = value;
    }

    public T X
    {
        get => data[0];
        set => data[0] = value;
    }
    //the problem comes when the vector f e is a 2 dim, but it has a Z. Its dangerous to have it.
}




