// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        [Fact]
        public async Task ShouldAddLabCommandAsync()
        {
            // given
            LabCommand randomLabCommand = CreateRandomLabCommand();
            var inputLabCommand = randomLabCommand;
            var postedLabCommand = inputLabCommand;
            var expectedLabCommand = postedLabCommand.DeepClone();

            this.dmxApiBrokerMock.Setup(broker =>
                broker.PostLabCommandAsync(inputLabCommand))
                    .ReturnsAsync(postedLabCommand);

            // when
            LabCommand actualLabCommand =
                await this.labCommandService.AddLabCommandAsync(inputLabCommand);

            // then
            actualLabCommand.Should().BeEquivalentTo(expectedLabCommand);

            this.dmxApiBrokerMock.Verify(broker=>
                broker.PostLabCommandAsync(inputLabCommand),
                    Times.Once());

            this.dmxApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
