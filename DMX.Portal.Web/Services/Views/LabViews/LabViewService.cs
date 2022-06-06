// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Labs;
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

        public ValueTask<List<LabView>> RetrieveAllLabViewsAsync() =>
        TryCatch(async () =>
        {
            List<Lab> labs = await this.labService.RetrieveAllLabsAsync();

            return labs.Select(AsLabView).ToList();
        });

        private static Func<Lab, LabView> AsLabView =>
        lab => new LabView
        {
            Id = lab.Id,
            Name = lab.Name,
            Description = lab.Description,
            DmxVersion = "1.0",
            Status = (LabViewStatus)lab.Status,
            Devices = lab.Devices.Select(AsLabDeviceView).ToList()
        };

        private static Func<LabDevice, LabDeviceView> AsLabDeviceView =
        device => new LabDeviceView
        {
            Name = device.Name,
            Type = (LabDeviceTypeView)device.Type,
            PowerLevel = ConvertIntToPowerLevelView(device.PowerLevel)
        };

        private static PowerLevelView ConvertIntToPowerLevelView(int? powerLevelInt)
        {
            return powerLevelInt switch
            {
                >= 0 and < 33 => PowerLevelView.Low,
                >= 33 and < 66 => PowerLevelView.Medium,
                >= 66 and < 101 => PowerLevelView.High,
                _ => PowerLevelView.Unknown
            };
        }
    }
}