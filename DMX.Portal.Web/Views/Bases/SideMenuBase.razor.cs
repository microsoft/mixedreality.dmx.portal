// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class SideMenuBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
