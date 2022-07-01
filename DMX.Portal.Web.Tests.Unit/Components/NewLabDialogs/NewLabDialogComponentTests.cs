// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syncfusion.Blazor;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

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

        public static TheoryData<Xeption> DependencyValidationExceptions()
        {
            string[] randomLabViewNameErrorMessages = GetRandomErrorMessages();
            string[] randomLabViewDescriptionErrorMessages = GetRandomErrorMessages();
            var invalidLabViewNameException = new InvalidLabViewException();
            var invalidLabViewDescriptionException = new InvalidLabViewException();
            var invalidLabViewNameDescriptionException = new InvalidLabViewException();

            invalidLabViewNameException.AddData(
                key: nameof(LabView.Name),
                values: randomLabViewNameErrorMessages);

            invalidLabViewDescriptionException.AddData(
                key: nameof(LabView.Description),
                values: randomLabViewDescriptionErrorMessages);

            invalidLabViewNameDescriptionException.AddData(
                key: nameof(LabView.Name),
                values: randomLabViewNameErrorMessages);

            invalidLabViewNameDescriptionException.AddData(
                key: nameof(LabView.Description),
                values: randomLabViewDescriptionErrorMessages);

            return new TheoryData<Xeption>
            {
                new LabViewValidationException(invalidLabViewNameException),
                new LabViewValidationException(invalidLabViewDescriptionException),
                new LabViewValidationException(invalidLabViewNameDescriptionException),
            };
        }

        public static TheoryData<Xeption> DependencyExceptions()
        {
            var someException = new Xeption();

            return new TheoryData<Xeption>
            {
                new LabViewDependencyException(someException),
                new LabViewServiceException(someException)
            };
        }

        private static string GetRandomString() =>
            new MnemonicString(GetRandomNumber()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 10).GetValue();

        private static string[] GetRandomErrorMessages()
        {
            int randomCount = GetRandomNumber();

            return Enumerable.Range(start: 0, count: randomCount)
                .Select(item => GetRandomString())
                    .ToArray();
        }
    }
}
