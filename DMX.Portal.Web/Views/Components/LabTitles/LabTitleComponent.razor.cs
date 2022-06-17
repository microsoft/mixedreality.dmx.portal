// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.Statuses;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabTitles
{
    public partial class LabTitleComponent : ComponentBase
    {
        [Parameter]
        public LabView Lab { get; set; }

        public StatusComponent StatusComponent { get; set; }
        public LargeTitleBase LabTitle { get; set; }
        public StatusView Status { get; set; }

        protected override void OnInitialized() =>
            this.Status = GetStatusView(Lab.Status);

        private StatusView GetStatusView(LabStatusView labStatusView)
        {
            return labStatusView switch
            {
                LabStatusView.Reserved => StatusView.Reserved,
                LabStatusView.Offline => StatusView.Offline,
                _ => StatusView.Available
            };
        }
    }
}
