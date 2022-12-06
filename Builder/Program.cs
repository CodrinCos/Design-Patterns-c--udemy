ICar car = new CarProxy(new Driver(12));

car.Drive();

public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive()
    {
        Console.WriteLine("Car is being driven");
    }
}

public class Driver
{
    public int Age;
    public Driver(int age)
    {
        Age = age;
    }
}

public class CarProxy : ICar
{
    private Driver driver;
    private Car car = new Car();

    public CarProxy(Driver driver)
    {
        this.driver = driver;
    }

    public void Drive()
    {
        if (driver.Age >= 18)
        {
            car.Drive();
        }
        else
        {
            Console.WriteLine("too young");
        }
    }
}