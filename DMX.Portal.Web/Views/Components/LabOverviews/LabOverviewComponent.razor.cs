// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using DMX.Portal.Web.Views.Components.LabOverviewDetails;
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
        public LabOverviewDetailsComponent Details { get; set; }
    }
}
