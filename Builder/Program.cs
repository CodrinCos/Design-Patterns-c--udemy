

public class Switch
{
    public State State = new OffState();

    public void On() { State.On(this); }

    public void Off() { State.Off(this); }
}

public abstract class State
{
    public virtual void On(Switch sw)
    { }

    public virtual void Off(Switch sw) 
    { }
}

public class OnState: State
{
    public OnState()
    {
        Console.WriteLine("Light turned on");
    }
}

public class OffState : State
{
    public OffState()
    {
        Console.WriteLine("Light turned off");
    }
}