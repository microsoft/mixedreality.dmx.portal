// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviews
{
    public partial class LabOverviewComponentTests : TestContext
    {
        private IRenderedComponent<LabOverviewComponent> renderedLabOverviewComponent;

        private static LabView CreateRandomLabView() =>
                   CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}
