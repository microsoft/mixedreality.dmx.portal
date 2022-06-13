// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviewDetails;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviewDetails
{
    public partial class LabOverviewDetailsComponentTests : TestContext
    {
        private IRenderedComponent<LabOverviewDetailsComponent> renderedLabDetailsComponent;

        private static LabView CreateRandomLabView() =>
            CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}
