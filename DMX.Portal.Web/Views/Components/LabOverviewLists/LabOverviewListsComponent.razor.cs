// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Foundations.Labs;
using DMX.Portal.Web.Services.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviewLists
{
    public partial class LabOverviewListsComponent : ComponentBase
    {
        public List<LabView> Labs { get; set; }

        public ILabViewService LabViewService { get; set; }

        protected override void OnInitialized()
        {
            this.Labs = GetLabs();
        }

        private static List<LabView> GetLabs() => 
            new List<LabView>();
    }
}
