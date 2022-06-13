﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.LabOverviewDetails;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.LabOverviewDetails
{
    public partial class LabOverviewDetailsComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialLabOverviewDetailsComponent =
                new LabOverviewDetailsComponent();

            // then
            initialLabOverviewDetailsComponent.Lab
                .Should().BeNull();

            initialLabOverviewDetailsComponent.NameLabelCell
                .Should().BeNull();
            
            initialLabOverviewDetailsComponent.NameValueCell
                .Should().BeNull();
            
            initialLabOverviewDetailsComponent.DescriptionLabelCell
                .Should().BeNull();
            
            initialLabOverviewDetailsComponent.DescriptionValueCell
                .Should().BeNull();

            initialLabOverviewDetailsComponent.DmxVersionLabelCell
                .Should().BeNull();

            initialLabOverviewDetailsComponent.DmxVersionValueCell
                .Should().BeNull();
        }

        [Fact]
        public void ShouldRenderLabNameAndDescriptionAndDmxVersionDetails()
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;
            string expectedNameLabel = nameof(LabView.Name);
            string expectedNameValue = inputLabView.Name;
            string expectedDescriptionLabel = nameof(LabView.Description);
            string expectedDescriptionValue = inputLabView.Description;
            string expectedDmxVersionLabel = nameof(LabView.DmxVersion);
            string expectedDmxVersionValue = inputLabView.DmxVersion;

            ComponentParameter inputComponentParameters =
                ComponentParameter.CreateParameter(
                    name: nameof(LabOverviewDetailsComponent.Lab),
                    value: inputLabView);

            // when
            this.renderedLabDetailsComponent =
                RenderComponent<LabOverviewDetailsComponent>(
                    inputComponentParameters);

            // then
            this.renderedLabDetailsComponent.Instance.DetailsTable
                .Should().NotBeNull();

            this.renderedLabDetailsComponent.Instance.NameLabelCell.Value
                .Should().Be(expectedNameLabel);
            
            this.renderedLabDetailsComponent.Instance.NameValueCell.Value
                .Should().Be(expectedNameValue);
            
            this.renderedLabDetailsComponent.Instance.DescriptionLabelCell.Value
                .Should().Be(expectedDescriptionLabel);
            
            this.renderedLabDetailsComponent.Instance.DescriptionValueCell.Value
                .Should().Be(expectedDescriptionValue);

            this.renderedLabDetailsComponent.Instance.DmxVersionLabelCell.Value
                .Should().Be(expectedDmxVersionLabel);

            this.renderedLabDetailsComponent.Instance.DmxVersionValueCell.Value
                .Should().Be(expectedDmxVersionValue);
        }
    }
}
