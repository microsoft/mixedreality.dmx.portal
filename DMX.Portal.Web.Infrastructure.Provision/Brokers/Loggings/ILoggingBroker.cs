// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}
