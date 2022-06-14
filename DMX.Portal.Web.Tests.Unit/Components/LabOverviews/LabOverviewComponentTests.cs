// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviews
{
    public partial class LabOverviewComponentTests : TestContext
    {
        private IRenderedComponent<LabOverviewComponent> renderedLabOverviewComponent;

        private static LabView CreateRandomLabView() =>
                   CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();

        public static TheoryData AllStatuses()
        {
            return new TheoryData<LabStatusView, StatusView> {
                { LabStatusView.Available,  StatusView.Available },
                { LabStatusView.Offline,  StatusView.Offline},
                { LabStatusView.Reserved,  StatusView.Reserved },
            };
        }
    }
}
