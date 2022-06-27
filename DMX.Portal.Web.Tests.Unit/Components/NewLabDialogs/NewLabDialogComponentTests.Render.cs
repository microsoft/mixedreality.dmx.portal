// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.NewLabDialogs
{
    public partial class NewLabDialogComponentTests
    {
        [Fact]
        public void ShouldRenderDefaults()
        {
            // given . when
            var initialNewLabDialogComponent = new NewLabDialog();

            // then
            initialNewLabDialogComponent.LabViewService.Should().BeNull();
            initialNewLabDialogComponent.State.Should().Be(NewLabDialogComponentState.Loading);
            initialNewLabDialogComponent.Dialog.Should().BeNull();
            initialNewLabDialogComponent.LabName.Should().BeNull();
            initialNewLabDialogComponent.LabDescription.Should().BeNull();
            initialNewLabDialogComponent.IsVisible.Should().BeFalse();
            initialNewLabDialogComponent.LabView.Should().BeNull();
            initialNewLabDialogComponent.Exception.Should().BeNull();
            initialNewLabDialogComponent.ErrorMessage.Should().BeNull();
            initialNewLabDialogComponent.Spinner.Should().BeNull();
            initialNewLabDialogComponent.ContentValidationSummary.Should().BeNull();
        }

        [Fact]
        public void ShouldDisplayDialogIfOpenDialogIsClicked()
        {
            // given
            NewLabDialogComponentState expectedState =
                NewLabDialogComponentState.Content;

            var expectedLabView = new LabView();

            // when 
            this.renderedNewLabDialog = RenderComponent<NewLabDialog>();
            this.renderedNewLabDialog.Instance.OpenDialog();

            // then
            this.renderedNewLabDialog.Instance.State.Should().Be(expectedState);
            this.renderedNewLabDialog.Instance.LabViewService.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Dialog.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Dialog.IsVisible.Should().BeTrue();
            this.renderedNewLabDialog.Instance.Dialog.Title.Should().Be("Lab Details");
            this.renderedNewLabDialog.Instance.Dialog.ButtonTitle.Should().Be("Save New Lab");
            this.renderedNewLabDialog.Instance.LabName.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabDescription.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.IsVisible.Should().BeTrue();
            this.renderedNewLabDialog.Instance.LabView.Should().BeEquivalentTo(expectedLabView);
            this.renderedNewLabDialog.Instance.Spinner.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Spinner.IsVisible.Should().BeFalse();
            this.renderedNewLabDialog.Instance.ContentValidationSummary.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldSubmitLabViewAsync()
        {
            // given
            string randomLabName = GetRandomString();
            string randomLabDescription = GetRandomString();
            string inputLabName = randomLabName;
            string inputLabDescription = randomLabDescription;
            string expectedLabName = inputLabName;
            string expectedLabDescription = inputLabDescription;

            var expectedLabView = new LabView
            {
                Name = inputLabName,
                Description = inputLabDescription
            };

            // when
            this.renderedNewLabDialog =
                RenderComponent<NewLabDialog>();

            this.renderedNewLabDialog.Instance
                .OpenDialog();

            await this.renderedNewLabDialog.Instance.LabName
                .SetValueAsync(inputLabName);

            await this.renderedNewLabDialog.Instance.LabDescription
                .SetValueAsync(inputLabDescription);

            this.renderedNewLabDialog.Instance.Dialog
                .Click();

            // then
            this.renderedNewLabDialog.Instance.Dialog.IsVisible
                .Should().BeFalse();

            this.renderedNewLabDialog.Instance.LabView
                .Should().BeEquivalentTo(expectedLabView);

            this.labViewServiceMock.Verify(service =>
                service.AddLabViewAsync(expectedLabView),
                    Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
