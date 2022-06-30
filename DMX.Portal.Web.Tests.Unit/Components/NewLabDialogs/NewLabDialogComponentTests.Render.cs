// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.NewLabDialogs;
using FluentAssertions;
using Moq;
using Xunit;
using SharpStyles;
using SharpStyles.Models;

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
            initialNewLabDialogComponent.LabId.Should().BeNull();
            initialNewLabDialogComponent.LabDmxVersion.Should().BeNull();
            initialNewLabDialogComponent.LabName.Should().BeNull();
            initialNewLabDialogComponent.LabDescription.Should().BeNull();
            initialNewLabDialogComponent.IsVisible.Should().BeFalse();
            initialNewLabDialogComponent.LabView.Should().BeNull();
            initialNewLabDialogComponent.Exception.Should().BeNull();
            initialNewLabDialogComponent.ErrorMessage.Should().BeNull();
            initialNewLabDialogComponent.Spinner.Should().BeNull();
            initialNewLabDialogComponent.LabNameValidationSummary.Should().BeNull();
            initialNewLabDialogComponent.LabDescriptionValidationSummary.Should().BeNull();
            initialNewLabDialogComponent.LabIdContainer.Should().BeNull();
            initialNewLabDialogComponent.LabDmxVersionContainer.Should().BeNull();
            initialNewLabDialogComponent.LabNameContainer.Should().BeNull();
            initialNewLabDialogComponent.LabDescriptionContainer.Should().BeNull();
            initialNewLabDialogComponent.DependencyErrorMessageLabel.Should().BeNull();
            initialNewLabDialogComponent.StyleElement.Should().BeNull();
            initialNewLabDialogComponent.Style.Should().BeNull();
        }

        [Fact]
        public void ShouldSetupComponentStyles()
        {
            // given
            var expectedStyle = new NewLabDialogComponentStyle
            {
                NewLabTextbox = new SharpStyle
                {
                    PaddingBottom = "20px"
                },
                NewLabErrorMessage = new SharpStyle
                {
                    Color = "red"
                }
            };

            // when
            this.renderedNewLabDialog = RenderComponent<NewLabDialog>();

            // then
            this.renderedNewLabDialog.Instance.StyleElement
                .Should().NotBeNull();

            this.renderedNewLabDialog.Instance.Style
                .Should().BeEquivalentTo(expectedStyle);

            this.renderedNewLabDialog.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedStyle);
        }

        [Fact]
        public void ShouldDisplayDialogIfOpenDialogIsClicked()
        {
            // given
            NewLabDialogComponentState expectedState =
                NewLabDialogComponentState.Content;

            string expectedTextBoxCssClass = "new-lab-textbox";
            string expectedTextBoxBottomCssClass = "new-lab-textbox-bottom";
            string expectedDependencyErrorMessageLabelCssClass = "new-lab-error-message";
            string expectedDialogTitle = "New Lab";
            string expectedDialogButtonTitle = "Submit";
            string expectedDmxVersion = "1.0";

            var expectedLabView = new LabView
            {
                DmxVersion = expectedDmxVersion
            };

            // when 
            this.renderedNewLabDialog = RenderComponent<NewLabDialog>();
            this.renderedNewLabDialog.Instance.OpenDialog();

            // then
            this.renderedNewLabDialog.Instance.State.Should().Be(expectedState);
            this.renderedNewLabDialog.Instance.LabViewService.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Dialog.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Dialog.IsVisible.Should().BeTrue();
            this.renderedNewLabDialog.Instance.Dialog.Title.Should().Be(expectedDialogTitle);
            this.renderedNewLabDialog.Instance.Dialog.ButtonTitle.Should().Be(expectedDialogButtonTitle);
            this.renderedNewLabDialog.Instance.LabId.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabId.IsDisabled.Should().BeTrue();
            this.renderedNewLabDialog.Instance.LabDmxVersion.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabDmxVersion.Value.Should().Be(expectedDmxVersion);
            this.renderedNewLabDialog.Instance.LabDmxVersion.IsDisabled.Should().BeTrue();
            this.renderedNewLabDialog.Instance.LabName.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabDescription.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.IsVisible.Should().BeTrue();
            this.renderedNewLabDialog.Instance.LabView.Name.Should().Be(expectedLabView.Name);
            this.renderedNewLabDialog.Instance.LabView.Description.Should().Be(expectedLabView.Description);
            this.renderedNewLabDialog.Instance.LabView.DmxVersion.Should().Be(expectedLabView.DmxVersion);
            this.renderedNewLabDialog.Instance.LabView.Status.Should().Be(expectedLabView.Status);
            this.renderedNewLabDialog.Instance.LabView.Devices.Should().BeEquivalentTo(expectedLabView.Devices);
            this.renderedNewLabDialog.Instance.Spinner.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.Spinner.IsVisible.Should().BeFalse();
            this.renderedNewLabDialog.Instance.LabNameValidationSummary.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabDescriptionValidationSummary.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabIdContainer.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabIdContainer.CssClass.Should().Be(expectedTextBoxCssClass);
            this.renderedNewLabDialog.Instance.LabDmxVersionContainer.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabDmxVersionContainer.CssClass.Should().Be(expectedTextBoxCssClass);
            this.renderedNewLabDialog.Instance.LabNameContainer.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.LabNameContainer.CssClass.Should().Be(expectedTextBoxCssClass);
            this.renderedNewLabDialog.Instance.LabDescriptionContainer.Should().NotBeNull();
            this.renderedNewLabDialog.Instance.DependencyErrorMessageLabel.Should().NotBeNull();

            this.renderedNewLabDialog.Instance.LabDescriptionContainer.CssClass
                .Should().Be(expectedTextBoxBottomCssClass);

            this.renderedNewLabDialog.Instance.DependencyErrorMessageLabel.ClassName
                    .Should().Be(expectedDependencyErrorMessageLabelCssClass);

            Guid.TryParse(this.renderedNewLabDialog.Instance.LabId.Value, out Guid labIdGuid)
                .Should().BeTrue();

            labIdGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task ShouldSubmitLabViewAsync()
        {
            // given
            string randomLabName = GetRandomString();
            string randomLabDescription = GetRandomString();
            string inputLabName = randomLabName;
            string inputLabDescription = randomLabDescription;
            string inputDmxVersion = "1.0";
            string expectedLabName = inputLabName;
            string expectedLabDescription = inputLabDescription;

            var expectedLabView = new LabView
            {
                Name = inputLabName,
                Description = inputLabDescription,
                DmxVersion = inputDmxVersion
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

            this.renderedNewLabDialog.Instance.LabView.Name.Should().Be(expectedLabView.Name);
            this.renderedNewLabDialog.Instance.LabView.Description.Should().Be(expectedLabView.Description);
            this.renderedNewLabDialog.Instance.LabView.DmxVersion.Should().Be(expectedLabView.DmxVersion);
            this.renderedNewLabDialog.Instance.LabView.Status.Should().Be(expectedLabView.Status);
            this.renderedNewLabDialog.Instance.LabView.Devices.Should().BeEquivalentTo(expectedLabView.Devices);

            Guid.TryParse(this.renderedNewLabDialog.Instance.LabId.Value, out Guid labIdGuid)
                .Should().BeTrue();

            labIdGuid.Should().NotBe(Guid.Empty);

            this.labViewServiceMock.Verify(service =>
                service.AddLabViewAsync(
                    this.renderedNewLabDialog.Instance.LabView),
                        Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDisableControlAndDisplayLoadingOnSubmitAsync()
        {
            // given
            string randomLabName = GetRandomString();
            string randomLabDescription = GetRandomString();

            var someLabView = new LabView
            {
                Name = randomLabName,
                Description = randomLabDescription
            };

            this.labViewServiceMock.Setup(service =>
                service.AddLabViewAsync(It.IsAny<LabView>()))
                    .ReturnsAsync(
                        value: someLabView,
                        delay: TimeSpan.FromMilliseconds(500));

            // when 
            this.renderedNewLabDialog =
                RenderComponent<NewLabDialog>();

            this.renderedNewLabDialog.Instance
                .OpenDialog();

            await this.renderedNewLabDialog.Instance.LabName
                .SetValueAsync(randomLabName);

            await this.renderedNewLabDialog.Instance.LabDescription
                .SetValueAsync(randomLabDescription);

            this.renderedNewLabDialog.Instance.Dialog
                .Click();

            // then
            this.renderedNewLabDialog.Instance.LabName
                .IsDisabled.Should().BeTrue();

            this.renderedNewLabDialog.Instance.LabDescription
                .IsDisabled.Should().BeTrue();

            this.renderedNewLabDialog.Instance.Dialog.DialogButton
                .Disabled.Should().BeTrue();

            this.renderedNewLabDialog.Instance.Spinner.IsVisible
                .Should().BeTrue();

            this.labViewServiceMock.Verify(service =>
                service.AddLabViewAsync(
                    It.IsAny<LabView>()),
                        Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
