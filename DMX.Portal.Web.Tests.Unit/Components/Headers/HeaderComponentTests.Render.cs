// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Components.Headers;
using FluentAssertions;
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
    }
}
