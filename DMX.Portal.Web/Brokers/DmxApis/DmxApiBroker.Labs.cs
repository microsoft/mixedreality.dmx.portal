// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync() =>
            await new ValueTask<List<Lab>>(new List<Lab>
            {
                new Lab
                {
                    Id = Guid.NewGuid(),
                    Name = "My Lab",
                    Devices = new List<LabDevice>
                    {
                        new LabDevice
                        {
                            Name = "Some Device"
                        }
                    }
                },

                new Lab
                {
                    Id = Guid.NewGuid(),
                    Name = "My Lab 2",
                    Devices = new List<LabDevice>
                    {
                        new LabDevice
                        {
                            Name = "Some Device 2"
                        }
                    }
                }
            });

        public async ValueTask<Lab> PostLabAsync(Lab lab) =>
            await PostAsync<Lab>(LabsRelativeUrl, lab);
    }
}
