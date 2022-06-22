// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenLabIsNullAndLogItAsync()
        {
            // given
            Lab nullLab = null;
            
            var nullLabException = new NullLabException();
            var expectedLabValidationException = new LabValidationException(nullLabException);

            // when
            ValueTask<Lab> addLabTask = this.labService.AddLabAsync(nullLab);

            LabValidationException actualLabValidationException = 
                await Assert.ThrowsAsync<LabValidationException>(addLabTask.AsTask);

            // then
            actualLabValidationException.Should().BeEquivalentTo(expectedLabValidationException);

            loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedLabValidationException))),
                    Times.Once);

            dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Never);

            dmxApiBrokerMock.VerifyNoOtherCalls();
            loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
