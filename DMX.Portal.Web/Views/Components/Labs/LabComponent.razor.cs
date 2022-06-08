// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.Labs
{
    public partial class LabComponent : ComponentBase
    {
        [Inject]
        public ILabViewService LabViewService { get; set; }

        public List<LabView> LabViews { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LabViewService.RetrieveAllLabViewsAsync();
        }
    }
}
