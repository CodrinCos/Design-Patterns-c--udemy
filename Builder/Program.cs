
var ba = new BankAccount();

var commands = new List<BankAccountCommand>
{
    new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
    new BankAccountCommand(ba, BankAccountCommand.Action.Withdrow, 50)
};

Console.WriteLine(ba);

foreach(var c in commands)
{
    c.Call();
    Console.WriteLine(ba);
}

foreach(var c in Enumerable.Reverse(commands))
{
    c.Undo();
    Console.WriteLine(ba);
}

public class BankAccount
{
    private int balance;
    private int overdraftLimit = -500;

    public void Deposit(int amount)
    {
        balance += amount;
        Console.WriteLine($"Deposit {amount}, balance is now {balance}");
    }

    public bool Withdrow(int amount)
    {
        if ( balance - amount >= overdraftLimit )
        {
            balance -= amount;
            Console.WriteLine($"Withdrew {amount}, balance is now {balance}");
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"{nameof(balance)}: {balance}";
    }
}

public interface ICommand
{
    void Call();
    void Undo();
}

public class BankAccountCommand : ICommand
{
    private BankAccount account;
    private bool succeeded;
    public enum Action
    {
        Deposit, Withdrow
    }

    private Action action;
    private int amount;

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
        this.account = account;
        this.action = action;
        this.amount = amount;
    }

    public void Call()
    {
        switch(action)
        {
            case Action.Deposit:
                account.Deposit(amount); 
                succeeded= true;
                break;
            case Action.Withdrow:
                succeeded = account.Withdrow(amount); 
                break;
            default: break;
        }
    }

    public void Undo()
    {
        if (!succeeded) { return; }

        switch (action)
        {
            case Action.Deposit:
                account.Withdrow(amount); break;
            case Action.Withdrow:
                account.Deposit(amount); break;
            default: break;
        }
    }
}