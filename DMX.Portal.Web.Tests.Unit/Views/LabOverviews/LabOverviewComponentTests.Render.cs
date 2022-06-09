// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Views.LabOverviews
{
    public partial class LabOverviewComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultComponent()
        {
            // given . when
            var initialLabOverviewComponent =
                new LabOverviewComponent();

            // then
            initialLabOverviewComponent.LabView.Should().BeNull();
            initialLabOverviewComponent.LabTitle.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderLabViewNameAndDevices()
        {
            // given
            var expectedDevicesUrls =
                new List<string>
                {
                    "imgs/NUC.png",
                    "imgs/HoloLens.png",
                    "imgs/Phone.png"
                };

            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;

            inputLabView.Devices = new List<LabDeviceView>
            {
                new LabDeviceView
                {
                    Type = LabDeviceTypeView.HeadMountedDisplay
                },

                new LabDeviceView
                {
                    Type = LabDeviceTypeView.Phone
                },

                new LabDeviceView
                {
                    Type = LabDeviceTypeView.PC
                }
            };

            LabView expectedLabview = inputLabView.DeepClone();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.LabView),
                    value: inputLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabView
                .Should().BeEquivalentTo(expectedLabview);

            this.renderedLabOverviewComponent.Instance.LabTitle.Text
                .Should().BeEquivalentTo(expectedLabview.Name);

            List<IRenderedComponent<ImageBase>> allDeviceImages =
                this.renderedLabOverviewComponent.FindComponents<ImageBase>()
                    .ToList();

            allDeviceImages.Select(deviceImage => deviceImage.Instance.Url)
                .Should().BeEquivalentTo(expectedDevicesUrls);
        }

        [Theory]
        [MemberData(nameof(AllDevices))]
        public void ShouldRenderAppropirateDeviceImage(
            (LabDeviceTypeView DeviceTypeView, string Url) deviceTypeUrl)
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;

            inputLabView.Devices = new List<LabDeviceView>
            {
                new LabDeviceView
                {
                    Type = deviceTypeUrl.DeviceTypeView
                },
            };

            LabView expectedLabview = inputLabView.DeepClone();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.LabView),
                    value: inputLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabView
                .Should().BeEquivalentTo(expectedLabview);

            this.renderedLabOverviewComponent.Instance.LabTitle.Text
                .Should().BeEquivalentTo(expectedLabview.Name);

            IRenderedComponent<ImageBase> deviceImageComponent =
                this.renderedLabOverviewComponent.FindComponent<ImageBase>();

            deviceImageComponent.Instance.Url.Should().BeEquivalentTo(
                deviceTypeUrl.Url);
        }
    }
}
