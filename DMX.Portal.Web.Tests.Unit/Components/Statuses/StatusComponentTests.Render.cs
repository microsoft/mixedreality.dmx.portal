using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
