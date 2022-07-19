// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.Menus
{
    public class SideMenuComponentStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle SideMenuContainer { get; set; }

        [CssClass]
        public SharpStyle AllMenuOptions { get; set; }
    }
}
