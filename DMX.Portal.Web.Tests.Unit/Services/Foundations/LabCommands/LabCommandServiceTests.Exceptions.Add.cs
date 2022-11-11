// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalExceptionOnAddCommandIfCriticalErrorOccursAsync(
            Xeption criticalException)
        {
            // given
            LabCommand someLabCommand = CreateRandomLabCommand();

            var failedLabCommandDependencyException =
                new FailedLabCommandDependencyException(
                    criticalException);

            var expectedLabCommandDependencyException =
                new LabCommandDependencyException(
                    failedLabCommandDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(criticalException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(
                    someLabCommand);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyException.Should().BeEquivalentTo(
                expectedLabCommandDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabCommandDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync()
        {
            // given
            LabCommand inputLabCommand = CreateRandomLabCommand();
            var httpResponseException = new HttpResponseException();

            var failedLabDependencyException =
                new FailedLabCommandDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabCommandDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(inputLabCommand);

            LabCommandDependencyException actualLabCommandDependencyException =
                await Assert.ThrowsAsync<LabCommandDependencyException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandDependencyException.Should()
                .BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            LabCommand someCommand = CreateRandomLabCommand();
            var serviceException = new Exception();

            var failedLabCommandServiceException =
                new FailedLabCommandServiceException(serviceException);

            var expectedLabCommandServiceException =
                new LabCommandServiceException(failedLabCommandServiceException);
            
            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()))
                    .ThrowsAsync(serviceException);
            
            // when
            ValueTask<LabCommand> addLabCommandTask =
                this.labCommandService.AddLabCommandAsync(someCommand);

            LabCommandServiceException actualLabCommandServiceException =
                await Assert.ThrowsAsync<LabCommandServiceException>(
                    addLabCommandTask.AsTask);

            // then
            actualLabCommandServiceException.Should()
                .BeEquivalentTo(expectedLabCommandServiceException);
            
            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabCommandAsync(It.IsAny<LabCommand>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabCommandServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
