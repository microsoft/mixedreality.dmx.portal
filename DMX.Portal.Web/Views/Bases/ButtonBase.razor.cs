// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class ButtonBase
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        [Parameter]
        public bool isDisabled { get; set; }

        public void Click() => OnClick.Invoke();

        public void Disable()
        {
            this.isDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.isDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}
