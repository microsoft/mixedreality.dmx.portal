// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.Statuses
{
    public partial class StatusComponent : ComponentBase
    {
        [Parameter]
        public StatusView Status { get; set; }

        public ImageBase StatusImage { get; set; }
        public string StatusImageUrl { get; set; }

        protected override void OnInitialized() =>
            this.StatusImageUrl = GetStatusUrl(Status);

        private string GetStatusUrl(StatusView statusView)
        {
            return statusView switch
            {
                StatusView.Available => "imgs/Available.gif",
                StatusView.Offline => "imgs/Offline.gif",
                _ => "imgs/Reserved.gif"
            };
        }
    }
}
