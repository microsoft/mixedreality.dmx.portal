// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabTitles;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Components.LabTitles
{
    public partial class LabTitleComponentTests
    {
        private IRenderedComponent<LabTitleComponent> renderedLabTitleComponent;

        public static LabView CreateRandomLabView() =>
            CreateLabViewFiller().Create();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}