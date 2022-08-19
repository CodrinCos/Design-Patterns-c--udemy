//prev used 1 builder
//sometimes several builder are needed for building different aspects
//facade - ???


var pb = new PersonBuilder();
Person person = pb.Lives.At("123 London").In("asd").WithPostcode("asd")
               .Works.At("Company").AsA("Engineer").Earning(123123); 

Console.WriteLine(person);

public class Person
{
    //address
    public string StreetAddress, Postcode, City;

    //employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)} : {Postcode}, {nameof(CompanyName)} : {CompanyName} etc ..."; 
    }
}

public class PersonBuilder //facade - keeps a reference to the person thats builds up, -it allows you access to sub-builders
{
    //reference! - not works for example with struct
    protected Person Person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(Person); //this is one facet.
    public PersonAddressBuilder Lives => new PersonAddressBuilder(Person); //another facet

    //Implicit conversion operator to Person 
    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.Person;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    // might not work with a value type!
    public PersonAddressBuilder(Person person)
    {
        this.Person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        Person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostcode(string postcode)
    {
        Person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        Person.City = city;
        return this;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.Person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        Person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        Person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        Person.AnnualIncome = amount;
        return this;
    }
}