// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponentTests : TestContext
    {
        private IRenderedComponent<DeviceOverviewComponent> renderedDeviceOverviewComponent;

        public static TheoryData AllDeviceImages()
        {
            return new TheoryData<(LabDeviceTypeView, string)>
            {
                (LabDeviceTypeView.HeadMountedDisplay, "imgs/HoloLens.png"),
                (LabDeviceTypeView.Phone, "imgs/Phone.png"),
                (LabDeviceTypeView.PC, "imgs/NUC.png"),
                (LabDeviceTypeView.Other, "imgs/OtherDevice.png")
            };
        }

        public static TheoryData AllDevicePowerLevelImages()
        {
            return new TheoryData<(PowerLevelView, string)>
            {
                (PowerLevelView.Empty, "imgs/EmptyPowerLevel.png"),
                (PowerLevelView.Low, "imgs/LowPowerLevel.png"),
                (PowerLevelView.Medium, "imgs/MediumPowerLevel.png"),
                (PowerLevelView.High, "imgs/HighPowerLevel.png"),
                (PowerLevelView.Full, "imgs/FullPowerLevel.png"),
            };
        }

        private static LabDeviceView CreateRandomLabDeviceView() =>
            CreateLabDeviceViewFiller().Create();

        private static Filler<LabDeviceView> CreateLabDeviceViewFiller() =>
            new Filler<LabDeviceView>();
    }
}
