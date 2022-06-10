// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using FluentAssertions;
using Force.DeepCloner;
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
        }

        [Fact]
        public void ShouldRenderDeviceImage()
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            LabDeviceView expectedLabDeviceView =
                inputLabDeviceView.DeepClone();

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(
                    inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.Device
                .Should().BeEquivalentTo(expectedLabDeviceView);

            this.renderedDeviceOverviewComponent.Instance.Image
                .Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(AllDevices))]
        public void ShouldRenderAppropriateDeviceImage((LabDeviceTypeView LabDeviceTypeView, string Url) deviceTypeImage)
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            inputLabDeviceView.Type = deviceTypeImage.LabDeviceTypeView;

            string expectedImagePath = deviceTypeImage.Url;

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
        }
    }
}
