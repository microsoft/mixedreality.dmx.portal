// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.Labs;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Views.Components.Labs
{
    public partial class LabComponentTests : TestContext
    {
        private IRenderedComponent<LabComponent> renderedLabComponent;

        private static LabView CreateRandomLabViews() =>
            CreateLabViewFiller().Create();

        private static LabView CreateRandomLabView() =>
            CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}
