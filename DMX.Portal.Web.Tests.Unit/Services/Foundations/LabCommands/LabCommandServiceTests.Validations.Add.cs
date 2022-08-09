// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenLabCommandIsNullAndLogItAsync()
        {
            // given
            LabCommand nullLabCommand = null;

            var nullLabCommandException =
                new NullLabCommandException();

            var expectedLabCommandValidationException =
                new LabCommandValidationException(nullLabCommandException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(nullLabCommand);

            LabCommandValidationException actualLabCommandValidationException =
                await Assert.ThrowsAsync<LabCommandValidationException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandValidationException.Should()
                .BeEquivalentTo(expectedLabCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabCommandIsInvalidAndLogItAsync()
        {
            // given
            var invalidLabCommand = new LabCommand();

            var invalidLabCommandException =
                new InvalidLabCommandException();

            invalidLabCommandException.AddData(
                key: nameof(LabCommand.Id),
                values: "Id is required");

            invalidLabCommandException.AddData(
                key: nameof(LabCommand.LabId),
                values: "Id is required");

            invalidLabCommandException.AddData(
                key: nameof(LabCommand.CreatedDate),
                values: "Date is required");

            invalidLabCommandException.AddData(
                key: nameof(LabCommand.UpdatedDate),
                values: "Date is required");

            var expectedLabCommandValidationException =
                new LabCommandValidationException(
                    invalidLabCommandException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(
                    invalidLabCommand);

            LabCommandValidationException actualLabCommandValidationException =
                await Assert.ThrowsAsync<LabCommandValidationException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandValidationException.Should()
                .BeEquivalentTo(expectedLabCommandValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandValidationException))),
                        Times.Once);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dmxApiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
