// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialDeviceOverviewComponent =
                new DeviceOverviewComponent();

            // then
            initialDeviceOverviewComponent.Device
                .Should().BeNull();

            initialDeviceOverviewComponent.ImageUrl
                .Should().BeNull();

            initialDeviceOverviewComponent.PowerLevelImageUrl
                .Should().BeNull();

            initialDeviceOverviewComponent.DeviceLabel
                .Should().BeNull();

            initialDeviceOverviewComponent.Image
                .Should().BeNull();

            initialDeviceOverviewComponent.PowerLevelImage
                .Should().BeNull();

            initialDeviceOverviewComponent.Container
                .Should().BeNull();
        }

        [Fact]
        public void ShouldRenderDeviceName()
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            string expectedDeviceName = inputLabDeviceView.Name;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.DeviceLabel.Text
                .Should().BeEquivalentTo(expectedDeviceName);
        }

        [Theory]
        [MemberData(nameof(AllDeviceImages))]
        public void ShouldRenderDeviceImage(
            (LabDeviceTypeView LabDeviceTypeView, string Url) deviceTypeImage)
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            inputLabDeviceView.Type = deviceTypeImage.LabDeviceTypeView;
            string expectedImagePath = deviceTypeImage.Url;
            string expectedImageWidth = "150px";

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.Image.Url
                .Should().BeEquivalentTo(expectedImagePath);

            this.renderedDeviceOverviewComponent.Instance.Image.Width
                .Should().Be(expectedImageWidth);
        }

        [Theory]
        [MemberData(nameof(AllDevicePowerLevelImages))]
        public void ShouldRenderDevicePowerLevel(
            (PowerLevelView Type, string Url) devicePowerLevel)
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            inputLabDeviceView.PowerLevel = devicePowerLevel.Type;
            string expectedPowerLevelImageUrl = devicePowerLevel.Url;
            string expectedPowerLevelImageWidth = "15px";

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.PowerLevelImage.Url
                .Should().Be(expectedPowerLevelImageUrl);

            this.renderedDeviceOverviewComponent.Instance.PowerLevelImage.Width
                .Should().Be(expectedPowerLevelImageWidth);
        }

        [Fact]
        public void ShouldRenderContainer()
        {
            // given
            string expectedCssClass = "device-overview";

            LabDeviceView someLabDeviceView =
                CreateRandomLabDeviceView();

            ComponentParameter inputComponentParameter =
               ComponentParameter.CreateParameter(
                   name: nameof(DeviceOverviewComponent.Device),
                   value: someLabDeviceView);


            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.Container.CssClass
                .Should().Be(expectedCssClass);
        }
    }
}
