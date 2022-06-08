// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.Labs;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DMX.Portal.Web.Tests.Unit.Views.Components.Labs
{
    public partial class LabComponentTests : TestContext
    {
        private readonly Mock<ILabViewService> labViewServiceMock;
        private IRenderedComponent<LabComponent> renderedLabComponent;

        public LabComponentTests()
        {
            this.labViewServiceMock = new Mock<ILabViewService>();
            this.Services.AddTransient(service => this.labViewServiceMock.Object);
        }
    }
}
