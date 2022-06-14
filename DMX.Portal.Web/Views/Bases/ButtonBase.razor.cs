// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class ButtonBase
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string ClassName { get; set; }

    }
}
