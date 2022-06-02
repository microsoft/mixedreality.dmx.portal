// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IAppServicePlan> CreatePlanAsync(string planName, IResourceGroup resourceGroup)
        {
            return await azure.AppServices.AppServicePlans
                .Define(name: planName)
                .WithRegion(region: Region.USCentral)
                .WithExistingResourceGroup(group: resourceGroup)
                .WithPricingTier(pricingTier: PricingTier.StandardS1)
                .WithOperatingSystem(operatingSystem: OperatingSystem.Windows)
                .CreateAsync();
        }
    }
}