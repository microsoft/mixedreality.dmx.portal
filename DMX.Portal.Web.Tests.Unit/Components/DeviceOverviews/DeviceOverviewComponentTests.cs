// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponentTests : TestContext
    {
        private IRenderedComponent<DeviceOverviewComponent> renderedDeviceOverviewComponent;

        private static LabDeviceView CreateRandomLabDeviceView() =>
            CreateLabDeviceViewFiller().Create();

        private static Filler<LabDeviceView> CreateLabDeviceViewFiller() =>
            new Filler<LabDeviceView>();
    }
}
