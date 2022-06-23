using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Lab randomLab = new Lab()
            {
                Id = randomLabViewProperties.Id,
                Name = randomLabViewProperties.LabName,
                Description = randomLabViewProperties.LabDescription,
                Status = randomLabViewProperties.LabStatus,

                Devices = ((List<dynamic>)randomLabViewProperties.Devices).Select(item =>
                new LabDevice
                {
                    Name = item.DeviceName,
                    PowerLevel = item.PowerLevel,
                    Type = item.LabDeviceType
                }).ToList()
            };

            LabView randomLabView = new LabView()
            {
                Id = randomLabViewProperties.Id,
                Name = randomLabViewProperties.LabName,
                Description = randomLabViewProperties.LabDescription,
                Status = randomLabViewProperties.LabStatusView,
                DmxVersion = randomLabViewProperties.DmxVersion,
                Devices = ((List<dynamic>)randomLabViewProperties.Devices).Select(item =>
                new LabDeviceView
                {
                    Name = item.DeviceName,
                    PowerLevel = item.PowerLevelView,
                    Type = item.LabDeviceTypeView
                }).ToList()
            };

            Lab inputLab = randomLab;
            Lab addedLab = inputLab;
            LabView inputLabView = randomLabView;
            LabView expectedLabView = inputLabView.DeepClone();

            this.labServiceMock.Setup(service =>
                service.AddLabAsync(inputLab))
                    .ReturnsAsync(addedLab);

            // when
            LabView actualLabView =
                await this.labViewService.AddLabViewAsync(inputLabView);

            // then
            actualLabView.Should().BeEquivalentTo(expectedLabView);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(inputLab),
                    Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
