// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Infrastructure.Provision.Services.Processings.CloudManagements;

namespace DMX.Portal.Web.Infrastructure.Provision
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var cloudManagementProcessingService = new CloudManagementProcessingService();
            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}