// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Services.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.Labs
{
    public partial class LabComponent : ComponentBase
    {
        [Inject]
        public ILabViewService LabViewService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LabViewService.RetrieveAllLabViewsAsync();
        }
    }
}
