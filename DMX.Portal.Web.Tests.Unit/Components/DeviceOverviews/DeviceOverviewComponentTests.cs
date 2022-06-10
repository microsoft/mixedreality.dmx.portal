// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponentTests : TestContext
    {
        private IRenderedComponent<DeviceOverviewComponent> renderedDeviceOverviewComponent;

        public static TheoryData AllDevices()
        {
            return new TheoryData<(LabDeviceTypeView, string)>
            {
                (LabDeviceTypeView.HeadMountedDisplay, "imgs/HoloLens.png"),
                (LabDeviceTypeView.Phone, "imgs/Phone.png"),
                (LabDeviceTypeView.PC, "imgs/NUC.png"),
            };
        }

        private static LabDeviceView CreateRandomLabDeviceView() =>
            CreateLabDeviceViewFiller().Create();

        private static Filler<LabDeviceView> CreateLabDeviceViewFiller() =>
            new Filler<LabDeviceView>();
    }
}
