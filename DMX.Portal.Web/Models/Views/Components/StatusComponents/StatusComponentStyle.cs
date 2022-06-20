// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.StatusComponents
{
    public class StatusComponentStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle StatusImage { get; set; }
    }
}
