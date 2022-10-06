// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabCommandsRelativeUrl = "api/labcommands";

        public async ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand)
        {
            //await GetAccessTokenForScope("PostLabCommand");

            return await PostAsync(LabCommandsRelativeUrl, labCommand);
        }

        public async ValueTask<List<LabCommand>> GetAllLabCommandsAsync()
        {
            return new List<LabCommand>
            {
                new LabCommand
                {
                    Id = Guid.NewGuid(),
                    LabId = Guid.Parse("32B9E1C3-1604-431E-A97F-8B556EFA0E71"),
                    Results = "some results here.",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Arguments = "dotnet --list-sdks",
                    Notes = "MRShift Testing",
                    Status = CommandStatus.Completed,
                },
                new LabCommand
                {
                    Id = Guid.NewGuid(),
                    LabId = Guid.Parse("32B9E1C3-1604-431E-A97F-8B556EFA0E71"),
                    Results = "",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Arguments = "dotnet test",
                    Notes = "MRShift Testing",
                    Status = CommandStatus.Running,
                },
                new LabCommand
                {
                    Id = Guid.NewGuid(),
                    LabId = Guid.Parse("32B9E1C3-1604-431E-A97F-8B556EFA0E71"),
                    Results = "",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Arguments = "dotnet test",
                    Notes = "MRShift Testing",
                    Status = CommandStatus.Pending,
                },
                new LabCommand
                {
                    Id = Guid.NewGuid(),
                    LabId = Guid.Parse("32B9E1C3-1604-431E-A97F-8B556EFA0E71"),
                    Results = "Error: 123",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Arguments = "dotnet db-migration run",
                    Notes = "MRShift Testing",
                    Status = CommandStatus.Error,
                }
            };
        }
    }
}
