// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabDetails
{
    public partial class LabDetailsComponent : ComponentBase
    {
        [Parameter]
        public LabView Lab { get; set; }
    }
}
