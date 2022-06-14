// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.LabOverviewDetails;
using DMX.Portal.Web.Views.Components.Statuses;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviews
{
    public partial class LabOverviewComponent : ComponentBase
    {
        [Parameter]
        public LabView Lab { get; set; }

        public ContainerBase Container { get; set; }
        public ContainerBase DevicesContainer { get; set; }
        public ContainerBase LabOverviewDetailsContainer { get; set; }
        public ContainerBase LabTitleStatusContainer { get; set; }
        public StatusComponent StatusComponent { get; set; }
        public LargeTitleBase Name { get; set; }
        public LabOverviewDetailsComponent Details { get; set; }
        public StatusView LabStatus { get; set; }

        protected override void OnInitialized() =>
            this.LabStatus = GetLabStatus();

        private StatusView GetLabStatus()
        {
            return this.Lab.Status switch
            {
                LabStatusView.Available => StatusView.Available,
                LabStatusView.Offline => StatusView.Offline,
                _ => StatusView.Reserved
            };
        }
    }
}
