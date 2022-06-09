// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Components.Labs;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Views.Components.Labs
{
    public partial class LabComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultComponent()
        {
            // given . when
            var initialLabComponent = new LabComponent();

            // then
            initialLabComponent.LabView.Should().BeNull();
        }
    }
}
