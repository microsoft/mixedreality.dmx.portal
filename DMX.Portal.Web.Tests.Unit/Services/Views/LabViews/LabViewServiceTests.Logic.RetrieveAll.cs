// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
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
        public async Task ShouldRetrieveAllLabViewsAsync()
        {
            // given
            List<dynamic> randomLabViewProperties =
                CreateRandomLabViewProperties();

            List<Lab> randomLabs = randomLabViewProperties.Select(item =>
                new Lab
                {
                    Id = item.Id,
                    ExternalId = item.ExternalId,
                    Name = item.LabName,
                    Description = item.LabDescription,
                    Status = item.LabStatus,

                    Devices = ((List<dynamic>)item.Devices).Select(item =>
                        new LabDevice
                        {
                            Name = item.DeviceName,
                            PowerLevel = item.PowerLevel,
                            Type = item.LabDeviceType
                        }).ToList()
                }).ToList();

            List<LabView> randomLabViews = randomLabViewProperties.Select(item =>
                new LabView
                {
                    Id = item.Id,
                    ExternalId = item.ExternalId,
                    Name = item.LabName,
                    Description = item.LabDescription,
                    Status = item.LabStatusView,
                    DmxVersion = item.DmxVersion,

                    Devices = ((List<dynamic>)item.Devices).Select(item =>
                        new LabDeviceView
                        {
                            Name = item.DeviceName,
                            PowerLevel = item.PowerLevelView,
                            Type = item.LabDeviceTypeView
                        }).ToList()
                }).ToList();

            List<Lab> retrievedLabs = randomLabs;
            List<LabView> expectedLabViews = randomLabViews;

            this.labServiceMock.Setup(service =>
                service.RetrieveAllLabsAsync())
                    .ReturnsAsync(retrievedLabs);

            // when
            List<LabView> actualLabViews =
                await this.labViewService.RetrieveAllLabViewsAsync();

            // then
            actualLabViews.Should().BeEquivalentTo(expectedLabViews);

            this.labServiceMock.Verify(service =>
                service.RetrieveAllLabsAsync(),
                    Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllLabViewsOrderedByNameAsync()
        {
            // given
            List<dynamic> randomLabViewProperties =
                CreateRandomLabViewProperties();

            List<Lab> randomLabs = randomLabViewProperties.Select(item =>
                new Lab
                {
                    Id = item.Id,
                    ExternalId = item.ExternalId,
                    Name = item.LabName,
                    Description = item.LabDescription,
                    Status = item.LabStatus,

                    Devices = ((List<dynamic>)item.Devices).Select(item =>
                        new LabDevice
                        {
                            Name = item.DeviceName,
                            PowerLevel = item.PowerLevel,
                            Type = item.LabDeviceType
                        }).ToList()

                }).ToList();

            List<LabView> randomLabViews = randomLabViewProperties.Select(item =>
            {
                return new LabView
                {
                    Id = item.Id,
                    ExternalId = item.ExternalId,
                    Name = item.LabName,
                    Description = item.LabDescription,
                    Status = item.LabStatusView,
                    DmxVersion = item.DmxVersion,
                    Devices = ((List<dynamic>)item.Devices).Select(item =>
                    new LabDeviceView
                    {
                        Name = item.DeviceName,
                        PowerLevel = item.PowerLevelView,
                        Type = item.LabDeviceTypeView
                    }).ToList()
                };
            }).ToList();

            List<Lab> retrievedLabs = randomLabs;

            List<LabView> expectedLabViews =
                OrderLabViewsByName(randomLabViews.DeepClone());

            this.labServiceMock.Setup(service =>
                service.RetrieveAllLabsAsync())
                    .ReturnsAsync(retrievedLabs);

            // when
            List<LabView> actualLabViews =
                await this.labViewService.RetrieveAllLabViewsAsync();

            // then
            actualLabViews.Should().BeEquivalentTo(
                expectedLabViews,
                options => options.WithStrictOrdering());

            this.labServiceMock.Verify(service =>
                service.RetrieveAllLabsAsync(),
                    Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
