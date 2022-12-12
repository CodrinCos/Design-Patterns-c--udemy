
//mvvm

//model - don't want validation etc.
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Person
{
    public string FirstName, LastName;


}

//view = UI

//viewmodel - place for change notification
//replicating the interface
public class PersonViewModel
	: INotifyPropertyChanged
{
	private readonly Person person;
	public PersonViewModel(Person person)
	{
		this.person = person;
	}

	public string FirstName
	{
		get => person.FirstName;
		set
		{
			if (person.FirstName == value) return;
			person.FirstName = value;
			OnPropertyChanged();
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string LastName
    {
        get => person.LastName;
        set
        {
            if (person.LastName == value) return;
            person.LastName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FullName));
        }
    }

    //if we add this will become a decorator and not a proxy anymore
    public string FullName
    {
        get => $"{FirstName} {LastName}".Trim();
        set
        {
            if (value == null)
            {
                FirstName = LastName = null;
                return;
            }
            var items = value.Split();

            if (items.Length > 0)
            {
                FirstName = items[0];
            }

            if (items.Length > 1)
            {
                FirstName = items[1];
            }
            OnPropertyChanged(nameof(FullName));

        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(
      [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this,
          new PropertyChangedEventArgs(propertyName));
    }
}