using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
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
            var nullLabViewException = new NullLabViewException();
            var expectedLabViewValidationException = new LabViewValidationException(nullLabViewException);

            // when
            ValueTask<LabView> addLabViewTask =
                this.labViewService.AddLabViewAsync(nullLabView);

            // then
            await Assert.ThrowsAsync<LabViewValidationException>(addLabViewTask.AsTask);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedLabViewValidationException))),
                    Times.Once);

            this.labServiceMock.Verify(service =>
                service.AddLabAsync(It.IsAny<Lab>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.labServiceMock.VerifyNoOtherCalls();
        }
    }
}
