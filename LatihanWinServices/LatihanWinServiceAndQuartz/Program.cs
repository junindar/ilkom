using Topshelf;

namespace LatihanWinServiceAndQuartz
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(configurator =>
            {
                configurator.SetServiceName("Ilmu Komputer Service Quartz");
                configurator.SetDescription("Ini adalah latihan membuat windows service menggunakan .Net Core dan Quartz");

                configurator.Service<JobService>(serviceConfigurator =>
                {
                   serviceConfigurator.ConstructUsing(() => new JobService());

                    serviceConfigurator.WhenStarted((service, hostControl) =>
                    {
                        service.OnStart();
                        return true;
                    });
                    serviceConfigurator.WhenStopped((service, hostControl) =>
                    {
                        service.OnStop();
                        return true;
                    });
                });
            });
        }
    }
}
