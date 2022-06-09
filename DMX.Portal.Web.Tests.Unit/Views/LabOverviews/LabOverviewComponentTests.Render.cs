// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Reflection.Metadata;
using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Views.LabOverviews
{
    public partial class LabOverviewComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultComponent()
        {
            // given . when
            var initialLabOverviewComponent =
                new LabOverviewComponent();

            // then
            initialLabOverviewComponent.LabView.Should().BeNull();
            initialLabOverviewComponent.LabTitle.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderLabViewName()
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;
            LabView expectedLabview = inputLabView.DeepClone();

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewComponent.LabView),
                    value: inputLabView);

            // when
            this.renderedLabOverviewComponent =
                RenderComponent<LabOverviewComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabOverviewComponent.Instance.LabView
                .Should().BeEquivalentTo(expectedLabview);

            this.renderedLabOverviewComponent.Instance.LabTitle.Text
                .Should().BeEquivalentTo(expectedLabview.Name);
        }
    }
}
