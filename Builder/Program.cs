var room = new ChatRoom();

var john = new Person("jhon");
var jahne = new Person("jane");

room.Join(john);
room.Join(jahne);

john.Say("hi");

jahne.Say("oh, hey JOHN");

var simon = new Person("simon");

room.Join(simon);

public class Person
{
    public string Name;
    public ChatRoom Room;

    private List<string> chatLog = new List<string>();

    public Person(string name)
    {
        Name= name;
    }

    public void Say(string message)
    {
        Room.Broadcast(Name, message);
    }

    public void PrivateMessage(string who, string message)
    {
        Room.Message(Name, who, message);
    }

    public void Receive(string sender, string message)
    {
        string s = $"{sender} : {message}";
        chatLog.Add(s);
        Console.WriteLine($"[{Name}'s chat session] {s}");
    }
}

//The mediator
public class ChatRoom
{
    private List<Person> people= new List<Person>();

    public void Join(Person p)
    {
        string joinMsg = $"{p.Name} joins the chat";
        Broadcast("room", joinMsg);
        p.Room = this;
        people.Add(p);
    }

    public void Broadcast(string source, string message)
    {
        foreach(var p in people)
        {
            if (p.Name != source)
            {
                p.Receive(source, message);
            }
        }
    }

    public void Message(string source, string destination, string message) 
    {
        people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
    }
}