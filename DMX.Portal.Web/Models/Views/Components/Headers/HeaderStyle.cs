// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.Headers
{
    public class HeaderStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle TopRow { get; set; }
    }
}
