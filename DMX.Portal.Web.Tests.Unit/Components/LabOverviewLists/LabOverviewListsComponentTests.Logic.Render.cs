// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Bunit;
using DMX.Portal.Web.Models.Views.Components.LabOverviewListComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.LabOverviewLists;
using DMX.Portal.Web.Views.Components.LabOverviews;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
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
        public void ShouldRenderLoading()
        {
            // given
            string expectedLoadingLabel = "Loading...";

            LabOverviewListComponentState expectedState =
                LabOverviewListComponentState.Loading;

            // when
            this.renderedLabOverviewListsComponent =
                RenderComponent<LabOverviewListsComponent>();

            // then
            this.renderedLabOverviewListsComponent.Instance.State
                .Should().Be(expectedState);

            IRenderedComponent<LabelBase> loadingLabel =
                this.renderedLabOverviewListsComponent.FindComponent<LabelBase>();

            loadingLabel.Instance.Text.Should().Be(expectedLoadingLabel);

            this.renderedLabOverviewListsComponent.Instance.Labs
                .Should().BeNull();

            IEnumerable<IRenderedComponent<LabOverviewComponent>> labComponents =
                this.renderedLabOverviewListsComponent
                    .FindComponents<LabOverviewComponent>();

            labComponents.Should().BeEmpty();

            this.labViewServiceMock.Verify(service =>
                service.RetrieveAllLabViewsAsync(),
                    Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
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

            this.labViewServiceMock.Setup(service =>
                service.RetrieveAllLabViewsAsync())
                    .ReturnsAsync(retrievedLabViews);

            LabOverviewListComponentState expectedState =
                LabOverviewListComponentState.Content;

            // when
            this.renderedLabOverviewListsComponent =
                RenderComponent<LabOverviewListsComponent>();

            // then
            this.renderedLabOverviewListsComponent.Instance.State
                .Should().Be(expectedState);

            this.renderedLabOverviewListsComponent.Instance.Labs
                .Should().BeEquivalentTo(expectedLabViews);

            IEnumerable<IRenderedComponent<LabOverviewComponent>> labComponents =
                this.renderedLabOverviewListsComponent
                    .FindComponents<LabOverviewComponent>();

            labComponents.Select(labComponent => labComponent.Instance.Lab)
                .Should().BeEquivalentTo(expectedLabViews);

            this.labViewServiceMock.Verify(service =>
                service.RetrieveAllLabViewsAsync(),
                    Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRenderError()
        {
            // given
            var exception = new Exception();

            this.labViewServiceMock.Setup(service =>
                service.RetrieveAllLabViewsAsync())
                    .Throws(exception);

            LabOverviewListComponentState expectedState =
                LabOverviewListComponentState.Error;

            string expectedErrorMessage = "An error has occured, contact support";

            // when
            this.renderedLabOverviewListsComponent =
                RenderComponent<LabOverviewListsComponent>();

            // then
            this.renderedLabOverviewListsComponent.Instance.State
                .Should().Be(expectedState);

            IRenderedComponent<LabelBase> errorLabel =
                this.renderedLabOverviewListsComponent.FindComponent<LabelBase>();

            errorLabel.Instance.Text
                .Should().Be(expectedErrorMessage);

            this.labViewServiceMock.Verify(service =>
                service.RetrieveAllLabViewsAsync(),
                    Times.Once);

            this.labViewServiceMock.VerifyNoOtherCalls();
        }
    }
}
