var log = new ConsoleLog();
//get an exception. I need a log which does nothing otherwise I have problems
//var ba = new BankAccount(null);

var ba = new BankAccount(new NullLog());

ba.Deposit(100);


public interface ILog
{
    void Info(string msg);
    void Warn(string msg);

    public static ILog Null => NullLog.Instance;

    private sealed class NullLog : ILog
    {
        private NullLog() { }

        private static Lazy<NullLog> instance = new (() => new NullLog());

        public static ILog Instance => instance.Value;

        public void Info(string msg)
        {
        }

        public void Warn(string msg)
        {
        }
    }
}

class ConsoleLog : ILog
{
    public void Info(string msg)
    {
        Console.WriteLine(msg);
    }

    public void Warn(string msg)
    {
        Console.WriteLine("Warning!!!" + msg);
    }
}

//It's not always safe becasuse sometimes you have props, or returns 
//class NullLog : ILog
//{
//    public void Info(string msg)
//    {
//    }

//    public void Warn(string msg)
//    {
//    }
//}


//Singleton
//public sealed class NullLog : ILog
//{
//    private NullLog() { }

//    public static Lazy<NullLog> instance;

//    public static ILog Instance => instance.Value;

//    public void Warn(string msg) { }

//    public void Info(string msg) { }
//}

public class BankAccount
{
    ILog log;
    int balance;


    public BankAccount(ILog log)
    {
        this.log = log;
    }

    public void Deposit(int amount)
    {
        balance += amount;
        log.Info($"Deposited {amount}, balance is now {balance} ");
    }
}