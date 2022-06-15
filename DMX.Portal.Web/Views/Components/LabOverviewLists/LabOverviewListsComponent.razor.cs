// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using DMX.Portal.Web.Models.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviewLists
{
    public partial class LabOverviewListsComponent : ComponentBase
    {
        public List<LabView> Labs { get; set; }
    }
}
