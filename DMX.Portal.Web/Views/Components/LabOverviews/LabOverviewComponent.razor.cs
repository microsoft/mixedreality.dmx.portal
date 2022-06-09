// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviews
{
    public partial class LabOverviewComponent
    {
        [Parameter]
        public LabView LabView { get; set; }

        public LargeTitleBase LabTitle { get; set; }
    }
}
