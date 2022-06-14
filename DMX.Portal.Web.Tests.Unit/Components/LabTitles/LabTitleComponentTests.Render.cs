// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Components.LabTitles;
using FluentAssertions;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabTitles
{
    public partial class LabTitleComponentTests
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            var initialLabTitleComponent = new LabTitleComponent();

            initialLabTitleComponent.Status
                .Should().Be(StatusView.Available);

            initialLabTitleComponent.Lab.Should().BeNull();
            initialLabTitleComponent.StatusComponent.Should().BeNull();
            initialLabTitleComponent.LabTitle.Should().BeNull();
        }
    }
}
