// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.NewLabDialogs
{
    public partial class NewLabDialogComponentTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldRenderValidationDetailsOnLabAsync(
            Xeption labViewValidationException)
        {
            // given
            string someLabName = GetRandomString();
            string someLabDescription = GetRandomString();

            this.labViewServiceMock.Setup(service =>
                service.AddLabViewAsync(It.IsAny<LabView>()))
                    .ThrowsAsync(labViewValidationException);

            // when
            this.renderedNewLabDialog =
                RenderComponent<NewLabDialog>();

            this.renderedNewLabDialog.Instance
                .OpenDialog();

            await this.renderedNewLabDialog.Instance.LabName
                .SetValueAsync(someLabName);

            await this.renderedNewLabDialog.Instance.LabDescription
                .SetValueAsync(someLabDescription);

            this.renderedNewLabDialog.Instance.Dialog
                .Click();

            // then
            this.renderedNewLabDialog.Instance.Dialog.IsVisible
                .Should().BeTrue();

            this.renderedNewLabDialog.Instance.LabName.IsDisabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.LabDescription.IsDisabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.Dialog.DialogButton.Disabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.Spinner.IsVisible
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.LabNameValidationSummary.ValidationData
                .Should().BeEquivalentTo(
                    labViewValidationException.InnerException.Data);

            this.renderedNewLabDialog.Instance.LabNameValidationSummary.Key
                .Should().Be(nameof(this.renderedNewLabDialog.Instance.LabView.Name));

            this.renderedNewLabDialog.Instance.LabNameValidationSummary.Color
                .Should().Be("Red");

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.ValidationData
                .Should().BeEquivalentTo(
                    labViewValidationException.InnerException.Data);

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.Key
                .Should().Be(nameof(this.renderedNewLabDialog.Instance.LabView.Description));

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.Color
                .Should().Be("Red");

            this.labViewServiceMock.Verify(service =>
                service.AddLabViewAsync(
                    It.IsAny<LabView>()),
                        Times.Once());

            this.labViewServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldRenderDependencyErrorDetailsOnLabAsync(
            Xeption labViewDependencyException)
        {
            // given
            string someLabName = GetRandomString();
            string someLabDescription = GetRandomString();

            this.labViewServiceMock.Setup(service =>
                service.AddLabViewAsync(It.IsAny<LabView>()))
                    .ThrowsAsync(labViewDependencyException);

            // when
            this.renderedNewLabDialog =
                RenderComponent<NewLabDialog>();

            this.renderedNewLabDialog.Instance
                .OpenDialog();

            await this.renderedNewLabDialog.Instance.LabName
                .SetValueAsync(someLabName);

            await this.renderedNewLabDialog.Instance.LabDescription
                .SetValueAsync(someLabDescription);

            this.renderedNewLabDialog.Instance.Dialog
                .Click();

            // then
            this.renderedNewLabDialog.Instance.Dialog.IsVisible
                .Should().BeTrue();

            this.renderedNewLabDialog.Instance.LabName.IsDisabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.LabDescription.IsDisabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.Dialog.DialogButton.Disabled
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.Spinner.IsVisible
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.LabNameValidationSummary.Key
                .Should().Be(nameof(this.renderedNewLabDialog.Instance.LabView.Name));

            this.renderedNewLabDialog.Instance.LabNameValidationSummary
                .ValidationData.Count.Should().Be(0);

            this.renderedNewLabDialog.Instance.LabNameValidationSummary.Color
                .Should().Be("Red");

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.Key
                .Should().Be(nameof(this.renderedNewLabDialog.Instance.LabView.Description));

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary
                .ValidationData.Count.Should().Be(0);

            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.Color
                .Should().Be("Red");

            this.labViewServiceMock.Verify(service =>
                service.AddLabViewAsync(
                    It.IsAny<LabView>()),
                        Times.Once());

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
