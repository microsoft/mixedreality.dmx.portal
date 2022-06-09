// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace DMX.Portal.Web.Infrastructure.Provision.Services.Processings.CloudManagements
{
    internal interface ICloudManagementProcessingService
    {
        ValueTask ProcessAsync();
    }
}
