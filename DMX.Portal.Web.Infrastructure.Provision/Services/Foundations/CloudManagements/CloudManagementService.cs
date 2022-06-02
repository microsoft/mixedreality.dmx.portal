// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds;
using DMX.Portal.Web.Infrastructure.Provision.Brokers.Loggings;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService()
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName}...");
            IResourceGroup resourceGroup = await this.cloudBroker.CreateResourceGroupAsync(resourceGroupName);
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName} completed.");

            return resourceGroup;
        }

        public async ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string planName = $"{projectName}-PLAN-{environment}";
            this.loggingBroker.LogActivity(message: $"Provisioning {planName}...");
            IAppServicePlan appServicePlan = await this.cloudBroker.CreatePlanAsync(planName, resourceGroup);
            this.loggingBroker.LogActivity(message: $"Provisioning {planName} completed.");

            return appServicePlan;
        }

        public async ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            IAppServicePlan servicePlan,
            IResourceGroup resourceGroup)
        {
            string appName = $"{projectName}-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {appName}...");

            IWebApp webapp = await this.cloudBroker.CreateWebAppAsync(
                appName,
                servicePlan,
                resourceGroup);

            this.loggingBroker.LogActivity(message: $"Provisioning {appName} completed.");

            return webapp;
        }
    }
}