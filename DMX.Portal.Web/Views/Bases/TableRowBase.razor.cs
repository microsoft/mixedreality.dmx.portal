// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class TableRowBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
