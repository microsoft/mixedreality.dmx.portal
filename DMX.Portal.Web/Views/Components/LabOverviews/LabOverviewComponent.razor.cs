// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.LabOverviews;
using DMX.Portal.Web.Models.Services.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.LabOverviewDetails;
using DMX.Portal.Web.Views.Components.LabTitles;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

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
        public LabTitleComponent LabTitleComponent { get; set; }
        public LabOverviewDetailsComponent Details { get; set; }
        public StyleBase StyleElement { get; set; }
        public LabOverviewStyle LabOverviewStyle { get; set; }

        protected override void OnInitialized() => SetupStyles();

        public void SetupStyles()
        {
            this.LabOverviewStyle = new LabOverviewStyle
            {
                LabOverview = new SharpStyle
                {
                    Border = "1px solid lightgrey",
                    Display = "flex",
                    Width = "100%",
                    BoxShadow = "1px 1px lightgrey",
                    MarginBottom = "5px"
                },

                DeviceOverviews = new SharpStyle
                {
                    Display = "flex",
                    Width = "100%",
                    Padding = "11px"
                },

                LabOverviewDetails = new SharpStyle
                {
                    Width = "100%",
                    Padding = "25px"
                },

                LabOverviewTitle = new SharpStyle
                {
                    Display = "flex"
                }
            };
        }
    }
}
