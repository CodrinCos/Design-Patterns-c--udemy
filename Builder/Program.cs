using Autofac;


var b = new ContainerBuilder();

b.RegisterType<ReportingService>().Named<IReportingService>("reporting");

b.RegisterDecorator<IReportingService>((context, service) => new ReportingServiceWithLogging(service), "reporting" );

using(var container = b.Build())
{
    var r = container.Resolve<IReportingService>();
    r.Report();
}

public interface IReportingService
{
    void Report();
}


public class ReportingService : IReportingService
{
    public void Report()
    {
        Console.WriteLine("Here is the report");
    }
}


public class ReportingServiceWithLogging : IReportingService
{
    private IReportingService decorated;

    public ReportingServiceWithLogging(IReportingService decorated)
    {
        this.decorated = decorated;
    }

    public void Report()
    {
        Console.WriteLine("this is a log");
        decorated.Report();
        Console.WriteLine("this is the end of a log");
    }
}