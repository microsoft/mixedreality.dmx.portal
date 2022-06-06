// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Foundations.Labs;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public partial class LabViewService : ILabViewService
    {
        private readonly ILabService labService;
        private readonly ILoggingBroker loggingBroker;

        public LabViewService(
            ILabService labService,
            ILoggingBroker loggingBroker)
        {
            this.labService = labService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<LabView>> RetrieveAllLabViewsAsync()
        {
            var labs = await this.labService.RetrieveAllLabsAsync();

            var labViews = labs.Select(lab =>
                new LabView
                {
                    Id = lab.Id,
                    Name = lab.Name,
                    Description = lab.Description,
                    DmxVersion = "1.0",
                    Status = (LabViewStatus)lab.Status,
                    Devices = lab.Devices.Select(device =>
                        new LabDeviceView
                        {
                            Name = device.Name,
                            Type = (LabDeviceTypeView)device.Type,
                            PowerLevel = ConvertIntToPowerLevelView(device.PowerLevel)
                        }).ToList<LabDeviceView>()
                }).ToList<LabView>();

            return labViews;
        }

        private PowerLevelView ConvertIntToPowerLevelView(int? powerLevelInt)
        {
            return powerLevelInt switch
            {
                int i when i >= 0 && i < 33 => PowerLevelView.Low,
                int i when i >= 33 && i < 66 => PowerLevelView.Medium,
                int i when i >= 66 && i < 101 => PowerLevelView.High,
                _ => PowerLevelView.Unknown,
            };
        }
    }
}