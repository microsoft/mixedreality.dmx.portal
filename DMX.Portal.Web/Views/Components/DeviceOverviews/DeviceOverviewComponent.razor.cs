// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponent
    {
        [Parameter]
        public LabDeviceView Device { get; set; }

        public ImageBase Image { get; set; }

        protected override Task OnInitializedAsync()
        {
            this.Image = new ImageBase();
            return base.OnInitializedAsync();
        }
    }
}
