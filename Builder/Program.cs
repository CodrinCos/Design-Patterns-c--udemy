
using NUnit.Framework;

[TestFixture]
public class Demo
{
	[Test]
	public void TestUser() //1655033
	{
		var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
		var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

		var users = new List<User>();

		foreach(var firstname in firstNames)
			foreach(var lastname in lastNames)
				users.Add(new User($"{firstname} {lastname}"));

		ForceGC();

		//add the nuget package and check for memory...
    }

	//testUser2 - 1296991

	private void ForceGC()
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		GC.Collect();
	}

	private string RandomString()
	{
		Random rand = new Random();

		return new string(Enumerable.Range(0, 10).Select(i => (char)('a' + rand.Next(26))).ToArray());
	}
}

public class User
{
    string fullName;

	public User(string fullName)
	{
		this.fullName = fullName;
	}
}

public class User2
{
	static List<string> strings = new List<string>();
	private int[] names;

	public User2(string fullName)
	{
		int getOrAdd(string s)
		{
			int idx = strings.IndexOf(s);
			if (idx != -1) return idx;
			else
			{
				strings.Add(s);
				return strings.Count - 1;
			}
		}

		names = fullName.Split(' ').Select(getOrAdd).ToArray();
	}

	public string FullName => string.Join(" ", names.Select(i => strings[i]));
}



//dotMemoryUnit - free framework that checks for the memory. (from JetBrains)