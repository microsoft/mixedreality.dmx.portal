// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrievalIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // given
            var failedExternalLabDependencyException = new FailedLabDependencyException(criticalDependencyException);
            var expectedLabDependencyException = new LabDependencyException(failedExternalLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(criticalDependencyException);

            // when
            ValueTask<List<Lab>> getAllLabsTask = this.labService.RetrieveAllLabsAsync();

            // then
            await Assert.ThrowsAsync<LabDependencyException>(() =>
                getAllLabsTask.AsTask());

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
            string someMessage = GetRandomString();
            var someResponseMessage = new HttpResponseMessage();
            HttpResponseException httpResponseException = new HttpResponseException(someResponseMessage, someMessage);

            // given
            var failedExternalLabDependencyException = new FailedLabDependencyException(httpResponseException);
            var expectedLabDependencyException = new LabDependencyException(failedExternalLabDependencyException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Lab>> getAllLabsTask = this.labService.RetrieveAllLabsAsync();

            // then
            await Assert.ThrowsAsync<LabDependencyException>(() =>
                getAllLabsTask.AsTask());

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
            string someMessage = GetRandomString();
            var serviceException = new Exception(someMessage);

            // given
            var failedExternalLabServiceException = new FailedLabServiceException(serviceException);
            var expectedLabServiceException = new LabServiceException(failedExternalLabServiceException);

            this.dmxApiBrokerMock.Setup(broker =>
                broker.GetAllLabsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Lab>> getAllLabsTask = this.labService.RetrieveAllLabsAsync();

            // then
            await Assert.ThrowsAsync<LabServiceException>(() =>
                getAllLabsTask.AsTask());

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
