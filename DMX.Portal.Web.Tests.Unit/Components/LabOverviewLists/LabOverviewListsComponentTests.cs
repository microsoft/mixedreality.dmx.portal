// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviewLists;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviewLists
{
    public partial class LabOverviewListsComponentTests : TestContext
    {
        private readonly Mock<ILabViewService> labViewServiceMock;
        IRenderedComponent<LabOverviewListsComponent> renderedLabOverviewListsComponent;

        public LabOverviewListsComponentTests()
        {
            this.labViewServiceMock = new Mock<ILabViewService>();
            this.Services.AddTransient(service => this.labViewServiceMock.Object);
        }

        private static List<LabView> CreateRandomLabViews() =>
            CreateLabViewFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Filler<LabView> CreateLabViewFiller() =>
            new Filler<LabView>();
    }
}
