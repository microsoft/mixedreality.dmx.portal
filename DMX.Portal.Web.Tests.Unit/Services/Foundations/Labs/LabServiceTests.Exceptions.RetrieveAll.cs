// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.Labs;
using DMX.Portal.Web.Models.Services.Foundations.Labs.Exceptions;
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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrievalIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var failedLabDependencyException =
                new FailedLabDependencyException(criticalDependencyException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Lab>> getAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(() =>
                    getAllLabsTask.AsTask());

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrievalIfErrorOccursAndLogItAsync()
        {
            // given
            string someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(someResponseMessage, someMessage);

            var failedLabDependencyException =
                new FailedLabDependencyException(httpResponseException);

            var expectedLabDependencyException =
                new LabDependencyException(failedLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Lab>> getAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabDependencyException actualLabDependencyException =
                await Assert.ThrowsAsync<LabDependencyException>(() =>
                    getAllLabsTask.AsTask());

            // then
            actualLabDependencyException
                .Should().BeEquivalentTo(expectedLabDependencyException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabDependencyException))),
                        Times.Once);

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrievalIfErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedLabServiceException =
                new FailedLabServiceException(serviceException);

            var expectedLabServiceException =
                new LabServiceException(failedLabServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Lab>> getAllLabsTask =
                this.labService.RetrieveAllLabsAsync();

            LabServiceException actualLabServiceException =
                await Assert.ThrowsAsync<LabServiceException>(() =>
                    getAllLabsTask.AsTask());

            // then
            actualLabServiceException
                .Should().BeEquivalentTo(expectedLabServiceException);

            this.dmxApiBrokerMock.Verify(broker =>
                broker.GetAllLabsAsync(),
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
