// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Foundations.Labs;
using DMX.Portal.Web.Services.Views.LabViews;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Services.Views.LabViews
{
    public partial class LabViewServiceTests
    {
        private readonly Mock<ILabService> labServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly ILabViewService labViewService;

        public LabViewServiceTests()
        {
            this.labServiceMock = new Mock<ILabService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.labViewService = new LabViewService(
                labService: this.labServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyExceptions()
        {
            var innerException = new Xeption();

            return new TheoryData<Xeption>()
            {
                new LabDependencyException(innerException),
                new LabServiceException(innerException),
            };
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private Expression<Func<Lab, bool>> SameLabAs(Lab expectedLab) =>
            actualLab => this.compareLogic.Compare(expectedLab, actualLab).AreEqual;

        private static List<dynamic> CreateRandomLabViewProperties()
        {
            return Enumerable.Range(start: 0, count: GetRandomNumber())
                .Select(item =>
                {
                    Guid randomId = Guid.NewGuid();

                    (LabStatus labStatus, LabStatusView labStatusView) =
                        GetRandomCorrespondingEnums<LabStatus, LabStatusView>();

                    return new
                    {
                        Id = randomId,
                        ExternalId = GetRandomString(),
                        LabName = GetRandomString(),
                        LabDescription = GetRandomString(),
                        DmxVersion = "1.0",
                        LabStatusView = labStatusView,
                        LabStatus = labStatus,
                        Devices = CreateRandomLabDevices()
                    };
                }).ToList<dynamic>();
        }

        private static dynamic CreateRandomLabViewProperty()
        {
            Guid randomId = Guid.NewGuid();

            (LabStatus labStatus, LabStatusView labStatusView) =
                GetRandomCorrespondingEnums<LabStatus, LabStatusView>();

            return new
            {
                Id = Guid.NewGuid(),
                ExternalId = GetRandomString(),
                LabName = GetRandomString(),
                LabDescription = GetRandomString(),
                DmxVersion = "1.0",
                LabStatusView = labStatusView,
                LabStatus = labStatus,
                Devices = CreateRandomLabDevices()
            };
        }

        private static List<dynamic> CreateRandomLabDevices()
        {
            return Enumerable.Range(start: 0, count: GetRandomNumber())
                .Select(item =>
                {
                    string randomDeviceName = GetRandomString();

                    (PowerLevelView powerLevelView, int? powerLevel) =
                        GetRandomPowerLevelRange();

                    (LabDeviceType randomLabDeviceType, LabDeviceTypeView randomLabDeviceTypeView) =
                        GetRandomCorrespondingEnums<LabDeviceType, LabDeviceTypeView>();

                    return new
                    {
                        DeviceName = randomDeviceName,
                        PowerLevelView = powerLevelView,
                        PowerLevel = powerLevel,
                        LabDeviceTypeView = randomLabDeviceTypeView,
                        LabDeviceType = randomLabDeviceType
                    };
                }).ToList<dynamic>();
        }

        private static LabView CreateRandomLabView() =>
            new Filler<LabView>().Create();

        private static List<LabView> OrderLabViewsByName(List<LabView> labs) =>
            labs.OrderBy(lab => lab.Name).ToList();

        private static (T, U) GetRandomCorrespondingEnums<T, U>()
        {
            int max = Enum.GetValues(typeof(T)).Length;
            int randomInt = new IntRange(min: 0, max).GetValue();
            var firstEnum = (T)(object)randomInt;
            var secondEnum = (U)(object)randomInt;

            return (firstEnum, secondEnum);
        }

        private static (PowerLevelView, int?) GetRandomPowerLevelRange()
        {
            int lowRange = GetRandomNumber(0, 33);
            int midRange = GetRandomNumber(33, 66);
            int highRange = GetRandomNumber(66, 101);

            var allPowerLevelViews = new List<(PowerLevelView, int?)>
            {
                new (PowerLevelView.Low, lowRange),
                new (PowerLevelView.Medium, midRange),
                new (PowerLevelView.High, highRange),
                new (PowerLevelView.Unknown, null)
            };

            return GetRandomPowerLevel(allPowerLevelViews);
        }

        private static (PowerLevelView, int?) GetRandomPowerLevel(
            List<(PowerLevelView, int?)> allPowerLevelViews)
        {
            return allPowerLevelViews
                .OrderBy(view => new Random().Next())
                    .First();
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetRandomNumber(int min, int max) =>
            new IntRange(min, max).GetValue();

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();
    }
}
