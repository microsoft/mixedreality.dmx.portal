// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Components.Menus;
using FluentAssertions;
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
        }
    }
}
