// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.Components.LabOverviewListComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviewLists
{
    public partial class LabOverviewListsComponent : ComponentBase
    {
        [Inject]
        public ILabViewService LabViewService { get; set; }

        public LabOverviewListComponentState State { get; set; }
        public List<LabView> Labs { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.Labs = await LabViewService.RetrieveAllLabViewsAsync();
                this.State = LabOverviewListComponentState.Content;
            }
            catch (Exception exception)
            {
                this.State = LabOverviewListComponentState.Error;
            }

            StateHasChanged();
        }
    }
}
