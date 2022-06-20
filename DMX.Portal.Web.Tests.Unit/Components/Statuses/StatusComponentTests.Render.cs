// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Components.Statuses;
using FluentAssertions;
using SharpStyles.Models;
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
            initialStatusComponent.Style.Should().BeNull();
            initialStatusComponent.StyleElement.Should().BeNull();
        }

        [Fact]
        public void ShouldSetupComponentStyles()
        {
            // given
            var expectedStyle = new StatusComponentStyle
            {
                StatusImage = new SharpStyle
                {
                    Height = "15px",
                    Width = "15px",
                    MarginRight = "9px",
                    MarginBottom = "3px"
                }
            };

            // when
            this.renderedStatusComponent =
                RenderComponent<StatusComponent>();

            // then
            this.renderedStatusComponent.Instance.Style
                .Should().BeEquivalentTo(expectedStyle);

            this.renderedStatusComponent.Instance.StyleElement
                .Should().NotBeNull();

            this.renderedStatusComponent.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedStyle);
        }


        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void ShouldRenderStatus(StatusView inputStatus, string expectedUrl)
        {
            // given
            string expectedCssClass = "status-image";

            ComponentParameter statusParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(StatusComponent.Status),
                    value: inputStatus);

            // when
            this.renderedStatusComponent =
                RenderComponent<StatusComponent>(
                    statusParameter);

            // then
            this.renderedStatusComponent.Instance.StatusImageUrl
                .Should().Be(expectedUrl);

            this.renderedStatusComponent.Instance.StatusImage
                .Should().NotBeNull();

            this.renderedStatusComponent.Instance.StatusImage.Url
                .Should().Be(expectedUrl);

            this.renderedStatusComponent.Instance.StatusImage.CssClass
                .Should().Be(expectedCssClass);
        }
    }
}
