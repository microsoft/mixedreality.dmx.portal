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

            return OrderLabViewsByName(
                labs.Select(AsLabViewWithDevices).ToList());
        });

        public ValueTask<LabView> AddLabViewAsync(LabView labView) =>
        TryCatch(async () =>
        {
            ValidateLabViewOnAdd(labView);

            Lab inputLab = AsLab(labView);
            Lab addedLab = await this.labService.AddLabAsync(inputLab);
            LabView addedLabView = AsLabView(addedLab);

            return addedLabView;
        });

        private static List<LabView> OrderLabViewsByName(
            List<LabView> labViews) =>
                labViews.OrderBy(labView => labView.Name).ToList();

        private static Func<Lab, LabView> AsLabView =>
        lab => new LabView
        {
            Id = lab.Id,
            ExternalId = lab.ExternalId,
            Name = lab.Name,
            Description = lab.Description,
            DmxVersion = "1.0",
            Status = (LabStatusView)lab.Status
        };

        private static Func<Lab, LabView> AsLabViewWithDevices =>
        lab => new LabView
        {
            Id = lab.Id,
            ExternalId = lab.ExternalId,
            Name = lab.Name,
            Description = lab.Description,
            DmxVersion = "1.0",
            Status = (LabStatusView)lab.Status,
            Devices = lab.Devices.Select(AsLabDeviceView).ToList()
        };

        private static Func<LabView, Lab> AsLab =>
        lab => new Lab
        {
            Id = lab.Id,
            ExternalId = lab.ExternalId,
            Name = lab.Name,
            Description = lab.Description,
            Status = (LabStatus)lab.Status
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
                0 => PowerLevelView.Empty,
                > 0 and < 33 => PowerLevelView.Low,
                >= 33 and < 66 => PowerLevelView.Medium,
                >= 66 and < 100 => PowerLevelView.High,
                100 => PowerLevelView.Full,
                _ => PowerLevelView.Unknown
            };
        }
    }
}