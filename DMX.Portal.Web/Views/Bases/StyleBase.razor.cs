// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Bases
{
    public partial class StyleBase : ComponentBase
    {
        [Parameter]
        public SharpStyle Style { get; set; }
    }
}
