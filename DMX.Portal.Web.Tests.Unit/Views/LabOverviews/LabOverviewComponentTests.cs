// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Views.LabOverviews
{
    public partial class LabOverviewComponentTests : TestContext
    {
        private IRenderedComponent<LabOverviewComponent> renderedLabOverviewComponent;

        public static TheoryData AllDevices()
        {
            return new TheoryData<(LabDeviceTypeView, string)>
            {
                (LabDeviceTypeView.HeadMountedDisplay, "imgs/HoloLens.png"),
                (LabDeviceTypeView.PC, "imgs/NUC.png"),
                (LabDeviceTypeView.Phone, "imgs/Phone.png")
            };
        }

        private static LabView CreateRandomLabView() =>
            CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}
