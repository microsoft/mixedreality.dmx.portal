using System;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public void LogActivity(string message) => Console.WriteLine(message);
    }
}
