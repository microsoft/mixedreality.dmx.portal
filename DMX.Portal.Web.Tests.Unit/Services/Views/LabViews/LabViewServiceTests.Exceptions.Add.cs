// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
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
        public async Task ShouldThrowDependencyExceptionOnAddIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            LabView randomLabView = CreateRandomLabView();
            LabView inputLabView = randomLabView;

            var expectedLabViewDependencyException =
                new LabViewDependencyException(
                    dependencyException.InnerException as Xeption);

            this.labServiceMock.Setup(service =>
                service.AddLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<LabView> labViewTask =
                this.labViewService.AddLabViewAsync(inputLabView);

            LabViewDependencyException actualLabViewDependencyException =
                await Assert.ThrowsAsync<LabViewDependencyException>(
                    labViewTask.AsTask);

            // then
            actualLabViewDependencyException.Should().BeEquivalentTo(
                expectedLabViewDependencyException);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.IsAny<Lab>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewDependencyException))),
                        Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccurrsAndLogItAsync()
        {
            // given
            var randomLabView = CreateRandomLabView();
            var inputLabView = randomLabView;
            var serviceException = new Exception();

            var failedLabViewServiceException =
                new FailedLabViewServiceException(serviceException);

            var expectedLabViewServiceException =
                new LabViewServiceException(failedLabViewServiceException);

            this.labServiceMock.Setup(service =>
                service.AddLabAsync(It.IsAny<Lab>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<LabView> addLabView = 
                this.labViewService.AddLabViewAsync(randomLabView);

            var actualLabViewServiceException =
                await Assert.ThrowsAsync<LabViewServiceException>(
                    addLabView.AsTask);

            // then
            actualLabViewServiceException
                .Should().BeEquivalentTo(expectedLabViewServiceException); 

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.IsAny<Lab>()), 
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewServiceException))),
                        Times.Once);

            this.labServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
