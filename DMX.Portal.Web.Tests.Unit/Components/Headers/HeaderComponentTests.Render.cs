// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.Headers;
using DMX.Portal.Web.Views.Components.Headers;
using FluentAssertions;
using SharpStyles.Models;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.Headers
{
    public partial class HeaderComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultValues()
        {
            // given . when
            var renderedHeaderComponent = new HeaderComponent();

            // then
            renderedHeaderComponent.Header.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderHeaderWithStyle()
        {
            // given
            var expectedHeaderStyle = new HeaderStyle
            {
                TopRow = new SharpStyle
                {
                    BackgroundColor = "#0078d4",
                    BorderBottom = "1px solid #d6d5d5",
                    JustifyContent = "flex-end",
                    Height = "3.5rem",
                    Display = "flex",
                    AlignItems = "center",
                }
            };

            // when
            this.renderedHeaderComponent = RenderComponent<HeaderComponent>();

            // then
            this.renderedHeaderComponent.Instance.Header.Should().NotBeNull();

            this.renderedHeaderComponent.Instance.Style
                .Should().BeEquivalentTo(expectedHeaderStyle);

            this.renderedHeaderComponent.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedHeaderStyle);
        }
    }
}
