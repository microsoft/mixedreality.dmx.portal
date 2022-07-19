// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class MenuOptionBase : ComponentBase
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string IconClass { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string ClassName { get; set; }
    }
}
