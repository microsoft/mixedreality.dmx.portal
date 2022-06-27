// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Views.LabViews;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Views.LabViews
{
    public partial class LabViewServiceTests
    {
        [Fact]
        public async Task ShouldAddLabViewAsync()
        {
            // given
            dynamic randomLabViewProperties =
                CreateRandomLabViewProperty();

            var randomLab = new Lab()
            {
                Id = randomLabViewProperties.Id,
                Name = randomLabViewProperties.LabName,
                Description = randomLabViewProperties.LabDescription,
                Status = randomLabViewProperties.LabStatus
            };

            var randomLabView = new LabView()
            {
                Id = randomLabViewProperties.Id,
                Name = randomLabViewProperties.LabName,
                Description = randomLabViewProperties.LabDescription,
                Status = randomLabViewProperties.LabStatusView,
                DmxVersion = randomLabViewProperties.DmxVersion
            };

            Lab inputLab = randomLab;
            Lab addedLab = inputLab;
            LabView inputLabView = randomLabView;
            LabView expectedLabView = inputLabView.DeepClone();

            this.labServiceMock.Setup(service =>
                service.AddLabAsync(It.Is(SameLabAs(inputLab))))
                    .ReturnsAsync(addedLab);

            // when
            LabView actualLabView =
                await this.labViewService.AddLabViewAsync(inputLabView);

            // then
            actualLabView.Should().BeEquivalentTo(expectedLabView);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.Is(SameLabAs(inputLab))),
                    Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
