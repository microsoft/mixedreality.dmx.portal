// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviews
{
    public partial class LabOverviewComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialLabOverviewComponent =
                new LabOverviewComponent();

            // then
            initialLabOverviewComponent.Lab.Should().BeNull();
            initialLabOverviewComponent.Details.Should().BeNull();
        }
    }
}
