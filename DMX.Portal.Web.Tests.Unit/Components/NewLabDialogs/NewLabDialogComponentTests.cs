// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;

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
            this.Services.AddOptions();
        }

        public static string GetRandomString() =>
            new MnemonicString(GetRandomNumber()).GetValue();

        public static int GetRandomNumber() =>
            new IntRange(min: 1, max: 10).GetValue();
    }
}
