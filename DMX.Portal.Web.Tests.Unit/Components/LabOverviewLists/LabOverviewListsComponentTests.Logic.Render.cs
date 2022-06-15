// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviewLists;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviewLists
{
    public partial class LabOverviewListsComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultValues()
        {
            // given . when
            var initialRenderedLabOverviewListsComponent =
                new LabOverviewListsComponent();

            // then
            initialRenderedLabOverviewListsComponent.LabViewService
                .Should().BeNull();

            initialRenderedLabOverviewListsComponent.Labs
                .Should().BeNull();
        }

        [Fact]
        public void ShouldRenderAllLabViews()
        {
            // given
            List<LabView> randomLabviews =
                CreateRandomLabViews();
            
            List<LabView> retrievedLabViews =
                randomLabviews;
            
            List<LabView> expectedLabViews =
                retrievedLabViews.DeepClone();

            // when
            this.renderedLabOverviewListsComponent =
                RenderComponent<LabOverviewListsComponent>();

            // then
            this.renderedLabOverviewListsComponent.Instance.Labs
                .Should().BeEquivalentTo(expectedLabViews);

            IEnumerable<IRenderedComponent<LabOverviewComponent>> labComponents =
                this.renderedLabOverviewListsComponent
                    .FindComponents<LabOverviewComponent>();

            labComponents.Select(labComponent => labComponent.Instance.Lab)
                .Should().BeEquivalentTo(expectedLabViews);
        }
    }
}
