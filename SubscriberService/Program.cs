using System.ServiceProcess;

namespace SubscriberService
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
                new SubscriberService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}