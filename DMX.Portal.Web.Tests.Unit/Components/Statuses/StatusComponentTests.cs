// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Components.Statuses;
using Tynamix.ObjectFiller;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.Statuses
{
    public partial class StatusComponentTests : TestContext
    {
        private IRenderedComponent<StatusComponent> renderedStatusComponent;

        public static TheoryData AllStatuses()
        {
            return new TheoryData<StatusView, string> {
                { StatusView.Available, "imgs/AvailableStatus.gif" },
                { StatusView.Offline, "imgs/OfflineStatus.gif" },
                { StatusView.Reserved, "imgs/ReservedStatus.gif" },
                { StatusView.Unregistered, "imgs/UnregisteredStatus.gif" }
            };
        }

        private static string GetRandomWidth()
        {
            int randomNumber =
                new IntRange(min: 1, max: 100).GetValue();

            return $"{randomNumber}px";
        }
    }
}
