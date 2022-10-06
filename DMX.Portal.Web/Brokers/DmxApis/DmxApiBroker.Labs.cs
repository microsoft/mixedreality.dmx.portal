// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.Labs;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync()
        {
            //await GetAccessTokenForScope("GetAllLabs");

            return new List<Lab>
            {
                new Lab
                {
                    Id = Guid.Parse("32B9E1C3-1604-431E-A97F-8B556EFA0E71"),
                    Name = "Lab 1",
                    Description = "some stuff",
                    Status = LabStatus.Available,
                    ExternalId = "NUC123",

                    Devices = new List<LabDevice>()
                }
            };
        }

        public async ValueTask<Lab> PostLabAsync(Lab lab)
        {
            //await GetAccessTokenForScope("PostLab");

            return await PostAsync<Lab>(LabsRelativeUrl, lab);
        }
    }
}
