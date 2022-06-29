// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Views.LabViews
{
    public partial class LabViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLabViewIsNullAndLogItAsync()
        {
            // given
            LabView nullLabView = null;

            var nullLabViewException =
                new NullLabViewException();

            var expectedLabViewValidationException =
                new LabViewValidationException(nullLabViewException);

            // when
            ValueTask<LabView> addLabViewTask =
                this.labViewService.AddLabViewAsync(nullLabView);

            LabViewValidationException actualLabViewValidationException =
                await Assert.ThrowsAsync<LabViewValidationException>(
                    addLabViewTask.AsTask);

            // then
            actualLabViewValidationException.Should().BeEquivalentTo(
                expectedLabViewValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewValidationException))),
                        Times.Once);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.IsAny<Lab>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.labServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfLabViewIsInvalidAndLogItAsync(
            string invalidString)
        {
            // given
            var invalidLabView = new LabView()
            {
                ExternalId = invalidString,
                Name = invalidString,
                Description = invalidString,
                DmxVersion = invalidString
            };

            var invalidLabViewException =
                new InvalidLabViewException();

            invalidLabViewException.AddData(
                key: nameof(LabView.Id),
                values: "Id is required");

            invalidLabViewException.AddData(
                key: nameof(LabView.ExternalId),
                values: "Id is required");

            invalidLabViewException.AddData(
                key: nameof(LabView.Name),
                values: "Text is required");

            invalidLabViewException.AddData(
                key: nameof(LabView.Description),
                values: "Text is required");

            invalidLabViewException.AddData(
                key: nameof(LabView.DmxVersion),
                values: "Text is required");

            var expectedLabViewValidationException =
                new LabViewValidationException(invalidLabViewException);

            // when
            ValueTask<LabView> addLabViewTask =
                this.labViewService.AddLabViewAsync(
                    invalidLabView);

            LabViewValidationException actualLabViewValidationException =
                await Assert.ThrowsAsync<LabViewValidationException>(
                    addLabViewTask.AsTask);

            // then
            actualLabViewValidationException.Should().BeEquivalentTo(
                expectedLabViewValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedLabViewValidationException))),
                        Times.Once);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.IsAny<Lab>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.labServiceMock.VerifyNoOtherCalls();
        }
    }
}
