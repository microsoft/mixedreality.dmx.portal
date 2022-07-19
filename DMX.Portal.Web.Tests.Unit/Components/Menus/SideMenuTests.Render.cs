// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.Menus;
using DMX.Portal.Web.Views.Components.Menus;
using FluentAssertions;
using SharpStyles.Models;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.Menus
{
    public partial class SideMenuTests
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialSideMenuComponent = new SideMenuComponent();

            // then
            initialSideMenuComponent.Container
                .Should().BeNull();

            initialSideMenuComponent.TitleContainer
                .Should().BeNull();

            initialSideMenuComponent.Style
                .Should().BeNull();

            initialSideMenuComponent.StyleElement
                .Should().BeNull();
        }

        [Fact]
        public void ShouldRenderSideMenuWithStyles()
        {
            // given
            var expectedSideMenuStyle = new SideMenuComponentStyle
            {
                SideMenuContainer = new SharpStyle
                {
                    BackgroundColor = "#faf9f8",
                    Height = "100%"
                }
            };

            // when
            this.renderedSideMenuComponent = RenderComponent<SideMenuComponent>();

            // then
            this.renderedSideMenuComponent.Instance.Container.CssClass.Should().Be("side-menu-container");
            this.renderedSideMenuComponent.Instance.TitleContainer.Should().NotBeNull();
            this.renderedSideMenuComponent.Instance.Style.Should().NotBeNull();
            this.renderedSideMenuComponent.Instance.StyleElement.Should().NotBeNull();

            this.renderedSideMenuComponent.Instance.Style
                .Should().BeEquivalentTo(expectedSideMenuStyle);

            this.renderedSideMenuComponent.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedSideMenuStyle);
        }
    }
}
