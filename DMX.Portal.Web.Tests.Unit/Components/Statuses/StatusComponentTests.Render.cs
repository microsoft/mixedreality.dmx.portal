// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Reflection.Metadata;
using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Components.Statuses;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.Statuses
{
    public partial class StatusComponentTests
    {
        [Fact]
        public void ShouldRenderDefaults()
        {
            // given . when
            var initialStatusComponent = new StatusComponent();

            // then
            initialStatusComponent.Status.Should().Be(StatusView.Available);
            initialStatusComponent.StatusImage.Should().BeNull();
            initialStatusComponent.StatusImageUrl.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void ShouldRenderStatus(StatusView inputStatus, string expectedUrl)
        {
            // given

            ComponentParameter inputParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(StatusComponent.Status),
                    value: inputStatus);

            // when
            this.renderedStatusComponent =
                RenderComponent<StatusComponent>(inputParameters);

            // then
            this.renderedStatusComponent.Instance.StatusImageUrl
                .Should().Be(expectedUrl);

            this.renderedStatusComponent.Instance.StatusImage
                .Should().NotBeNull();

            this.renderedStatusComponent.Instance.StatusImage.Url
                .Should().Be(expectedUrl);
        }
    }
}
