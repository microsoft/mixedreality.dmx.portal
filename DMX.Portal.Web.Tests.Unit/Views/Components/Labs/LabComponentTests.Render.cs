// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using AngleSharp.Dom;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.Labs;
using FluentAssertions;
using Force.DeepCloner;
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
            initialLabComponent.LabViews.Should().BeNull();
        }

        [Fact]
        public void ShouldRetrieveLabViewNames()
        {
            List<LabView> randomLabViews =
                CreateRandomLabViews();

            List<LabView> retrievedLabViews =
                randomLabViews;

            List<LabView> expectedLabViews =
                retrievedLabViews.DeepClone();

            this.labViewServiceMock.Setup(service =>
                service.RetrieveAllLabViewsAsync())
                    .ReturnsAsync(retrievedLabViews);

            // given . when
            this.renderedLabComponent = 
                RenderComponent<LabComponent>();

            // then
            this.renderedLabComponent.Instance.LabViewService
                .Should().NotBeNull();
            
            this.renderedLabComponent.Instance.LabViews
                .Should().BeEquivalentTo(expectedLabViews);

            IRefreshableElementCollection<IElement> allLabNames =
                this.renderedLabComponent.FindAll("lab-name");

            allLabNames.Count.Should().Be(expectedLabViews.Count);

            this.labViewServiceMock.Verify(service =>
                service.RetrieveAllLabViewsAsync(),
                    Times.Once());

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
