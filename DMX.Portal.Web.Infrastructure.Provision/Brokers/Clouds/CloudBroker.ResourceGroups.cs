// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace DMX.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName)
        {
            return await this.azure.ResourceGroups
                .Define(name: resourceGroupName)
                .WithRegion(region: Region.USCentral)
                .CreateAsync();
        }

        public async ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(name: resourceGroupName);

    }
}