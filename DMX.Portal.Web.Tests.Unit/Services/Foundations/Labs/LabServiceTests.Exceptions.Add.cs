// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var someLab = CreateRandomLab();

            var failedLabDependencyException =
                new FailedLabDependencyException(criticalDependencyException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(someLab);

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(addLabTask.AsTask);

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestExceptionOccursAndLogItAsync()
        {
            // given
            var inputLab = CreateRandomLab();
            var randomMessage = GetRandomString();
            var httpResponseMessage = new HttpResponseMessage();

            var randomDictionary = CreateRandomDictionary();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, randomMessage);

            httpResponseBadRequestException.AddData(randomDictionary);

            var invalidLabException =
                new InvalidLabException(httpResponseBadRequestException, randomDictionary);

            var expectedLabDependencyValidationException =
                new LabDependencyValidationException(invalidLabException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Lab> addLabTask = this.labService.AddLabAsync(inputLab);

            LabDependencyValidationException actualLabDependencyValidationException =
                await Assert.ThrowsAsync<LabDependencyValidationException>(addLabTask.AsTask);

            // then
            actualLabDependencyValidationException.Should().BeEquivalentTo(
                expectedLabDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabDependencyValidationException))),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfConflictExceptionOccursAndLogItAsync()
        {
            // given
            var inputLab = CreateRandomLab();
            var randomMessage = GetRandomString();
            var httpResponseMessage = new HttpResponseMessage();

            var randomDictionary = CreateRandomDictionary();

            var httpResponseConflictException =
                new HttpResponseConflictException(httpResponseMessage, randomMessage);

            httpResponseConflictException.AddData(randomDictionary);

            var invalidLabException =
                new AlreadyExistsLabException(httpResponseConflictException, randomDictionary);

            var expectedLabDependencyValidationException =
                new LabDependencyValidationException(invalidLabException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpResponseConflictException);

            // when
            ValueTask<Lab> addLabTask = this.labService.AddLabAsync(inputLab);

            LabDependencyValidationException actualLabDependencyValidationException =
                await Assert.ThrowsAsync<LabDependencyValidationException>(addLabTask.AsTask);

            // then
            actualLabDependencyValidationException.Should().BeEquivalentTo(
                expectedLabDependencyValidationException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedLabDependencyValidationException))),
                    Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            Lab inputLab = CreateRandomLab();
            string someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(someResponseMessage, someMessage);

            var failedLabDependencyException =
                new FailedLabDependencyException(httpResponseException);

            var expectedLabDependencyException = 
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(inputLab);

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(() =>
                    addLabTask.AsTask());

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfErrorOccursAndLogItAsync()
        {
            // given
            Lab someLab = CreateRandomLab();
            var serviceException = new Exception();

            var failedLabServiceException =
                new FailedLabServiceException(serviceException);

            var expectedLabServiceException =
                new LabServiceException(failedLabServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Lab> addLabTask =
                this.labService.AddLabAsync(someLab);

            LabServiceException actualLabServiceException =
                await Assert.ThrowsAsync<LabServiceException>(addLabTask.AsTask);

            // then
            actualLabServiceException
                .Should().BeEquivalentTo(expectedLabServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.PostLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabServiceException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
