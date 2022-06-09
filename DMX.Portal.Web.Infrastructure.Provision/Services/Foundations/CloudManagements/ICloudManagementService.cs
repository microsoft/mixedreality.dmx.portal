// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Portal.Web.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public interface ICloudManagementService
    {
        ValueTask<IResourceGroup> ProvisionResourceGroupAsync(string projectName, string environment);

        ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup);

        ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            IAppServicePlan servicePlan,
            IResourceGroup resourceGroup);

        ValueTask DeprovisionResourceGroupAsync(string projectName, string environment);
    }
}