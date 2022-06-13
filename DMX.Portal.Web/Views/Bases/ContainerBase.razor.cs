// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class ContainerBase : ComponentBase
    {
        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
