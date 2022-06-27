// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;

namespace DMX.Portal.Web.Tests.Unit.Components.NewLabDialogs
{
    public partial class NewLabDialogComponentTests : TestContext
    {
        private readonly Mock<ILabViewService> labViewServiceMock;
        private IRenderedComponent<NewLabDialog> renderedNewLabDialog;

        public NewLabDialogComponentTests()
        {
            this.labViewServiceMock = new Mock<ILabViewService>();
            this.Services.AddTransient(services => this.labViewServiceMock.Object);
            this.Services.AddSyncfusionBlazor();
        }
    }
}
