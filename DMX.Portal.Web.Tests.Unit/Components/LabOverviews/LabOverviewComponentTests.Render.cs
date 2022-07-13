// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.Components.LabOverviews;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using SharpStyles.Models;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviews
{
    public partial class LabOverviewComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialLabOverviewComponent =
                new LabOverviewComponent();

            // then
            initialLabOverviewComponent.Lab.Should().BeNull();
            initialLabOverviewComponent.Details.Should().BeNull();
            initialLabOverviewComponent.Container.Should().BeNull();
            initialLabOverviewComponent.DevicesContainer.Should().BeNull();
            initialLabOverviewComponent.LabOverviewDetailsContainer.Should().BeNull();
            initialLabOverviewComponent.LabTitleStatusContainer.Should().BeNull();
            initialLabOverviewComponent.LabTitleComponent.Should().BeNull();
            initialLabOverviewComponent.StyleElement.Should().BeNull();
            initialLabOverviewComponent.LabOverviewStyle.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderLabDevicesAndDetails()
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;

            ComponentParameter inputComponentParameters =
               ComponentParameter.CreateParameter(
                   name: nameof(LabOverviewComponent.Lab),
                   value: inputLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.Lab
                .Should().BeEquivalentTo(inputLabView);

            this.renderedLabOverviewComponent.Instance.Details.Lab
                .Should().BeEquivalentTo(inputLabView);

            IEnumerable<IRenderedComponent<DeviceOverviewComponent>> allDeviceOverviewComponents =
                this.renderedLabOverviewComponent.FindComponents<DeviceOverviewComponent>();

            allDeviceOverviewComponents.Select(deviceOverviewComponent =>
                deviceOverviewComponent.Instance.Device)
                    .Should().BeEquivalentTo(inputLabView.Devices);
        }

        [Fact]
        public void ShouldRenderContainerWithStyles()
        {
            // given
            string expectedCssClass = "lab-overview";
            LabView someLabView = CreateRandomLabView();

            var expectedStyle = new LabOverviewStyle
            {
                LabOverview = new SharpStyle
                {
                    Border = "1px solid lightgrey",
                    Display = "flex",
                    Width = "100%",
                    BoxShadow = "1px 1px lightgrey",
                    MarginBottom = "5px"
                },

                DeviceOverviews = new SharpStyle
                {
                    Display = "flex",
                    Width = "100%",
                    Padding = "11px"
                },

                LabOverviewDetails = new SharpStyle
                {
                    Width = "100%",
                    Padding = "25px"
                },

                LabOverviewTitle = new SharpStyle
                {
                    Display = "flex"
                }
            };

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.Container.CssClass
                .Should().Be(expectedCssClass);

            this.renderedLabOverviewComponent.Instance.LabOverviewStyle
                .Should().BeEquivalentTo(expectedStyle);

            this.renderedLabOverviewComponent.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedStyle);
        }

        [Fact]
        public void ShouldRenderDevicesContainer()
        {
            // given
            string expectedCssClass = "device-overviews";
            LabView someLabView = CreateRandomLabView();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.DevicesContainer.CssClass
                .Should().Be(expectedCssClass);
        }

        [Fact]
        public void ShouldRenderLabOverviewDetailsContainer()
        {
            // given
            string expectedCssClass = "lab-overview-details";
            LabView someLabView = CreateRandomLabView();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabOverviewDetailsContainer.CssClass
                .Should().Be(expectedCssClass);
        }

        [Fact]
        public void ShouldRenderLabTitleStatusContainer()
        {
            // given
            LabView someLabView = CreateRandomLabView();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabTitleStatusContainer
                .Should().NotBeNull();

            this.renderedLabOverviewComponent.Instance.LabTitleStatusContainer.CssClass
                .Should().Be("lab-overview-title");
        }

        [Fact]
        public void ShouldRenderLabTitleComponent()
        {
            // given
            LabView someLabView = CreateRandomLabView();
            LabView inputLabView = someLabView;

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabTitleComponent
                .Should().NotBeNull();

            this.renderedLabOverviewComponent.Instance.Lab
                .Should().BeEquivalentTo(inputLabView);
        }
    }
}
