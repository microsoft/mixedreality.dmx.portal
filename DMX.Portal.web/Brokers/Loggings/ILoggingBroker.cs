// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;

namespace DMX.Portal.Web.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogInformation (string message);
        void LogTrace (string message);
        void LogDebug (string message);
        void LogWarning (string message);
        void LogError (Exception exception);
        void LogCritical (Exception exception);
    }
}
