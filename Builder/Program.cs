//save for each step the memento and you can restore
//...



public class Memento
{
	public int Balance { get; }

	public Memento(int balance)
	{
		this.Balance = balance;
	}
}

public class BankAccount
{
    private int balance;
	public BankAccount(int banalce)
	{
		this.balance = banalce;
	}

    public Memento Deposit(int amount)
    {
        balance += amount;

		return new Memento(balance);
    }

	public void Restore(Memento m)
	{
		balance = m.Balance;
	}
}