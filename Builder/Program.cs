
using System.Collections;
using System.Collections.ObjectModel;

var neuron1 = new Neuron();
var neuron2 = new Neuron();

neuron1.ConnectTo(neuron2); // 1

var layer1 = new NeuronLayer();
var layer2 = new NeuronLayer();

// 4 - to connect neuron to neuron or to layers.

neuron1.ConnectTo(layer1);
layer1.ConnectTo(layer2);


public static class ExtensionMethods
{
    public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
    {
        if (ReferenceEquals(self, other)) return;

        foreach(var from in self)
        foreach(var to in other)
        {
            from.Out.Add(to);
            to.In.Add(to);
        }
    }
}

public class NeuronRing : List<Neuron>
{

}

public class NeuronLayer: Collection<Neuron>
{
    
}

public class Neuron : IEnumerable<Neuron>
{
    public float Value;

    public List<Neuron> In, Out;

    //return as an enumertator
    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

