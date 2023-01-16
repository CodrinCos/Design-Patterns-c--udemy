﻿//save for each step the memento and you can restore
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

	private List<Memento> changes = new List<Memento>();

	private int current;

	public BankAccount(int banalce)
	{
		this.balance = banalce;
		changes.Add(new Memento(balance));
	}

    public Memento Deposit(int amount)
    {
        balance += amount;

		var m = new Memento(balance);
		changes.Add(m);
		++current;

        return m;
    }

	public Memento Restore(Memento m)
	{
		if (m != null)
		{
			balance = m.Balance;
			changes.Add(m);
			return m;
		}

		return null;
	}

	public Memento Undo()
	{
		if (current > 0)
		{
			var m = changes[--current];

			balance= m.Balance;

			return m;
		}

		return null;
	}

	public Memento Redo()
	{
		if(current +1 < changes.Count)
		{
			var m = changes[++current];
			balance = m.Balance;
			return m;
		}
		return null;
	}
}