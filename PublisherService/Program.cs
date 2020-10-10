using System.ServiceProcess;

namespace PublisherService
{
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PublisherServise()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}