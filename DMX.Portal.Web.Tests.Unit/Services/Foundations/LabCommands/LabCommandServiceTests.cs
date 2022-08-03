// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Services.Foundations.LabCommands;
using Moq;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.LabCommands
{
    public partial class LabCommandServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabCommandService labCommandService;

        public LabCommandServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labCommandService = new LabCommandService(
                this.dmxApiBrokerMock.Object,
                this.loggingBrokerMock.Object);
        }

        private LabCommand CreateRandomLabCommand() =>
            CreateLabCommandFiller().Create();

        private Filler<LabCommand> CreateLabCommandFiller()
        {
            var filler = new Filler<LabCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset);

            return filler;
        }

        private DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

    }
}
