// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class ImageBase : ComponentBase
    {
        [Parameter]
        public string Url { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public string Height { get; set; }

        [Parameter]
        public string CssClass { get; set; }
    }
}
