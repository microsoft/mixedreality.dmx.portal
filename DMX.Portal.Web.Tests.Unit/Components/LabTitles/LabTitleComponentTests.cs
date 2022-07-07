// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabTitles;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabTitles
{
    public partial class LabTitleComponentTests : TestContext
    {
        private IRenderedComponent<LabTitleComponent> renderedLabTitleComponent;

        public static LabView CreateRandomLabView() =>
            CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();

        public static TheoryData AllStatuses()
        {
            return new TheoryData<LabStatusView, StatusView> {
                { LabStatusView.Available, StatusView.Available },
                { LabStatusView.Offline, StatusView.Offline },
                { LabStatusView.Reserved, StatusView.Reserved },
                { LabStatusView.Unregistered, StatusView.Unregistered }
            };
        }
    }
}