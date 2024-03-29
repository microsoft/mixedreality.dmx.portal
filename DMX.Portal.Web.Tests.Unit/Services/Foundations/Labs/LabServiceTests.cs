﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Services.Foundations.Labs;
using DMX.Portal.Web.Services.Foundations.Labs;
using Moq;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Foundations.Labs
{
    public partial class LabServiceTests
    {
        private readonly Mock<IDmxApiBroker> dmxApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabService labService;

        public LabServiceTests()
        {
            this.dmxApiBrokerMock = new Mock<IDmxApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labService = new LabService(
                dmxApiBroker: this.dmxApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyExceptions()
        {
            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualExpectedAssertException =>
                actualExpectedAssertException.Message == expectedException.Message &&
                actualExpectedAssertException.InnerException.Message == expectedException.InnerException.Message &&
                (actualExpectedAssertException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Lab CreateRandomLab() =>
            CreateLabFiller().Create();

        private static List<Lab> CreateRandomLabs() =>
            CreateLabFiller().Create(count: GetRandomNumber()).ToList();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetLocalRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber))
            {
                randomNumber = GetLocalRandomNumber();
            }

            return (T)(object)randomNumber;

            static int GetLocalRandomNumber() =>
                new IntRange(min: int.MinValue, max: int.MaxValue).GetValue();
        }

        private static Filler<Lab> CreateLabFiller() =>
            new Filler<Lab>();

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<string>>>();

            return filler.Create();
        }
    }
}
