// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Views.Components.LabDetails;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabDetails
{
    public partial class LabDetailsComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialLabDetailsComponent =
                new LabDetailsComponent();

            // then
            initialLabDetailsComponent.Lab
                .Should().BeNull();
        }
    }
}
