// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
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
            var webAppSettings = new Dictionary<string, string>
            {
                { "ASPNETCORE_ENVIRONMENT", ProjectEnvironment },
                { "ApiConfigurations:Url", this.dmxGatekeeperApiUrl},
                { "ApiConfiguration:AccessKey", this.dmxGatekeeperApiAccessKey},
                { "AzureAd:TenantId", this.tenantId},
                { "AzureAd:Instance", this.dmxPortalInstance},
                { "AzureAd:Domain", this.dmxPortalDomain},
                { "AzureAd:ClientId", this.dmxPortalClientId },
                { "AzureAd:CallbackPath", this.dmxPortalCallbackPath},
                { "DownstreamApi:BaseUrl", this.dmxGatekeeperApiUri},
                { "DownstreamApi:Scopes:GetAllLabs", this.dmxGatekeeperApiScopesGetAllLabs},
                { "DownstreamApi:Scopes:PostLab", this.dmxGatekeeperApiScopesPostLab},
            };

            return await azure.AppServices.WebApps
                .Define(name: webAppName)
                .WithExistingWindowsPlan(appServicePlan: appServicePlan)
                .WithExistingResourceGroup(group: resourceGroup)
                .WithNetFrameworkVersion(version: NetFrameworkVersion.Parse("v6.0"))
                .WithAppSettings(settings: webAppSettings)
                .CreateAsync();
        }
    }
}