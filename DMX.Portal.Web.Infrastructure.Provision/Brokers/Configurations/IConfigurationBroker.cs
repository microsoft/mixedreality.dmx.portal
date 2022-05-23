// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Infrastructure.Provision.Models.Configurations;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        public CloudManagementConfiguration GetConfiguration();
    }
}
