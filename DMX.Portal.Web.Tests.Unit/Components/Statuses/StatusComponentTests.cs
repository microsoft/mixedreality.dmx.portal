// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Components.Statuses;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.Statuses
{
    public partial class StatusComponentTests : TestContext
    {
        private IRenderedComponent<StatusComponent> renderedStatusComponent;

        public static TheoryData AllStatuses()
        {
            return new TheoryData<StatusView, string> {
                { StatusView.Available, "imgs/Available.gif" },
                { StatusView.Offline, "imgs/Offline.gif" },
                { StatusView.Reserved, "imgs/Reserved.gif" }
            };
        }
    }
}
