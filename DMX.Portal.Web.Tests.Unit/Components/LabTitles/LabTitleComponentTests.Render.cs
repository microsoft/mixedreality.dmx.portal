// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabTitles;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabTitles
{
    public partial class LabTitleComponentTests
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialLabTitleComponent = new LabTitleComponent();

            // then
            initialLabTitleComponent.Status
                .Should().Be(StatusView.Available);

            initialLabTitleComponent.Lab.Should().BeNull();
            initialLabTitleComponent.StatusComponent.Should().BeNull();
            initialLabTitleComponent.LabTitle.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderLabTitle()
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;
            string expectedLabName = inputLabView.Name;
            string expectedCssClass = "lab-title";

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabTitleComponent.Lab),
                    value: inputLabView);

            // when
            this.renderedLabTitleComponent =
                RenderComponent<LabTitleComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabTitleComponent.Instance.LabTitle.Text
                .Should().Be(expectedLabName);

            this.renderedLabTitleComponent.Instance.LabTitle.ClassName
                .Should().Be(expectedCssClass);
        }

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void ShouldRenderLabStatus(
            LabStatusView inputStatusView,
            StatusView expectedStatusView)
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            randomLabView.Status = inputStatusView;

            ComponentParameter inputLabParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(LabTitleComponent.Lab),
                    value: randomLabView);

            // when
            this.renderedLabTitleComponent =
                RenderComponent<LabTitleComponent>(
                    inputLabParameter);

            // then
            this.renderedLabTitleComponent.Instance.Status
                .Should().Be(expectedStatusView);

            this.renderedLabTitleComponent.Instance.StatusComponent.Status
                .Should().Be(expectedStatusView);
        }
    }
}
