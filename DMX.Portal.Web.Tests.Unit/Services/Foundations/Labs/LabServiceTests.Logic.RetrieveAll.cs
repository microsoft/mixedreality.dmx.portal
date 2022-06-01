// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllLabsAsync()
        {
            // given
            List<Lab> randomLabs = CreateRandomLabs();
            List<Lab> retrievedLabs = randomLabs;
            List<Lab> expectedLabs = retrievedLabs.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ReturnsAsync(retrievedLabs);

            // when
            List<Lab> actualLabs =
                await this.labService.RetrieveAllLabsAsync();

            // then
            actualLabs.Should().BeEquivalentTo(expectedLabs);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
