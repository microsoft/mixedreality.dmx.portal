// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Views.LabViews
{
    public partial class LabViewServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrievalIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            var expectedLabViewDependencyException =
                new LabViewDependencyException(
                    dependencyException.InnerException as Xeption);

            this.labServiceMock.Setup(service =>
                service.RetrieveAllLabsAsync())
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<List<LabView>> retrieveAllLabViews =
                this.labViewService.RetrieveAllLabViewsAsync();

            LabViewDependencyException actualLabViewDependencyException =
                await Assert.ThrowsAsync<LabViewDependencyException>(() =>
                    retrieveAllLabViews.AsTask());

            // then
            actualLabViewDependencyException
                .Should().BeEquivalentTo(expectedLabViewDependencyException);

            this.labServiceMock.Verify(service =>
                service.RetrieveAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewDependencyException))),
                        Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccurrsAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedLabViewServiceException =
                new FailedLabViewServiceException(serviceException);

            var expectedLabViewServiceException =
                new LabViewServiceException(failedLabViewServiceException);

            this.labServiceMock.Setup(service =>
                service.RetrieveAllLabsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<LabView>> retrieveAllLabViews =
                this.labViewService.RetrieveAllLabViewsAsync();

            LabViewServiceException actualLabViewServiceException =
                await Assert.ThrowsAsync<LabViewServiceException>(() =>
                    retrieveAllLabViews.AsTask());

            // then
            actualLabViewServiceException
                .Should().BeEquivalentTo(expectedLabViewServiceException);

            this.labServiceMock.Verify(service =>
                service.RetrieveAllLabsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewServiceException))),
                        Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
