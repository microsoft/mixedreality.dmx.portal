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

            var nullLabException =
                new NullLabException();

            var expectedLabValidationException =
                new LabValidationException(nullLabException);

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfLabIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            var invalidLab = new Lab()
            {
                Name = invalidString,
                Description = invalidString,
            };

            var invalidLabException = new InvalidLabException();

            invalidLabException.AddData(
                key: nameof(Lab.Id),
                values: "Id is required");

            invalidLabException.AddData(
                key: nameof(Lab.Name),
                values: "Text is required");

            invalidLabException.AddData(
                key: nameof(Lab.Description),
                values: "Text is required");

            var expectedLabValidationException =
                new LabValidationException(invalidLabException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(invalidLab);

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
