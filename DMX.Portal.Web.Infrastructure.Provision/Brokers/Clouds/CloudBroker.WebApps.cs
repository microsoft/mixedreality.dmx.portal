// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IWebApp> CreateWebAppAsync(
            string webAppName,
            IAppServicePlan appServicePlan,
            IResourceGroup resourceGroup)
        {
            return await azure.AppServices.WebApps
                .Define(name: webAppName)
                .WithExistingWindowsPlan(appServicePlan: appServicePlan)
                .WithExistingResourceGroup(group: resourceGroup)
                .WithNetFrameworkVersion(version: NetFrameworkVersion.Parse("v7.0"))
                .CreateAsync();
        }
    }
}