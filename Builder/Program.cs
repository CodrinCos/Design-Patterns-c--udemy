var log = new ConsoleLog();
//get an exception. I need a log which does nothing otherwise I have problems
//var ba = new BankAccount(null);

ba.Deposit(100);


public interface ILog
{
    void Info(string msg);
    void Warn(string msg);
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

class NullLog : ILog
{
    public void Info(string msg)
    {
    }

    public void Warn(string msg)
    {
    }
}

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