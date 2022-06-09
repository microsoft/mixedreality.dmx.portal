// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Infrastructure.Provision.Brokers.Configurations;
using DMX.Portal.Web.Infrastructure.Provision.Models.Configurations;
using DMX.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Services.Processings.CloudManagements
{
    internal class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly ICloudManagementService cloudManagementService;
        private readonly IConfigurationBroker configurationBroker;

        public CloudManagementProcessingService()
        {
            this.cloudManagementService = new CloudManagementService();
            this.configurationBroker = new ConfigurationBroker();
        }

        public async ValueTask ProcessAsync()
        {
            var configuration = this.configurationBroker.GetConfiguration();
            await ProvisionResourcesAsync(configuration);
            await DeprovisionResourcesAsync(configuration);

        }

        private async Task ProvisionResourcesAsync(CloudManagementConfiguration configuration)
        {
            string projectName = configuration.ProjectName;
            List<string> environments = RetrieveEnvironments(cloudAction: configuration.Up);

            foreach (string environment in environments)
            {
                IResourceGroup resourceGroup =
                    await cloudManagementService.ProvisionResourceGroupAsync(projectName, environment);

                IAppServicePlan servicePlan =
                    await cloudManagementService.ProvisionAppServicePlanAsync(projectName, environment, resourceGroup);

                IWebApp webApp =
                    await cloudManagementService.ProvisionWebAppAsync(projectName, environment, servicePlan, resourceGroup);
            }
        }

        private List<string> RetrieveEnvironments(CloudAction cloudAction) =>
            cloudAction?.Environments ?? new List<string>();

        private async Task DeprovisionResourcesAsync(CloudManagementConfiguration configuration)
        {
            var projectName = configuration.ProjectName;
            var environments = RetrieveEnvironments(cloudAction: configuration.Down);

            foreach (string environment in environments)
            {
                await cloudManagementService.DeprovisionResourceGroupAsync(projectName, environment);
            }
        }
    }
}
