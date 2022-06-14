// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
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
            initialLabOverviewComponent.StatusComponent.Should().BeNull();
            initialLabOverviewComponent.Name.Should().BeNull();
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
        public void ShouldRenderContainer()
        {
            // given
            string expectedCssClass = "lab-overview";
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
            this.renderedLabOverviewComponent.Instance.Container.CssClass
                .Should().Be(expectedCssClass);
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

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void ShouldRenderTitleStatuses(
            LabStatusView inputLabStatusView,
            StatusView expectedStatusView)
        {
            // given
            LabView someLabView = CreateRandomLabView();
            someLabView.Status = inputLabStatusView;
            string expectedLabName = someLabView.Name;

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.Lab),
                    value: someLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.StatusComponent.Status
                .Should().Be(expectedStatusView);

            this.renderedLabOverviewComponent.Instance.Name.Text
                .Should().Be(expectedLabName);
        }
    }
}
