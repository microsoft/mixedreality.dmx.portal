// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Components.Labs;
using FluentAssertions;
using Moq;
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
            initialLabComponent.LabViewService.Should().BeNull();
        }

        [Fact]
        public void ShouldCallLabViewServiceOnRender()
        {
            // given . when
            this.renderedLabComponent = RenderComponent<LabComponent>();

            // then
            this.renderedLabComponent.Instance.LabViewService.Should().NotBeNull();

            this.labViewServiceMock.Verify(service =>
                service.RetrieveAllLabViewsAsync(),
                    Times.Once());

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
