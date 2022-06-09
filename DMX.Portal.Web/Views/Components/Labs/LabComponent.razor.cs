// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.Labs
{
    public partial class LabComponent : ComponentBase
    {
        [Parameter]
        public LabView LabView { get; set; }
    }
}
