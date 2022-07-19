// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.Menus;
using DMX.Portal.Web.Views.Bases;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Components.Menus
{
    public partial class SideMenuComponent
    {
        public StyleBase StyleElement { get; set; }
        public SideMenuComponentStyle Style { get; set; }

        private void SetupStyle()
        {
            this.Style = new SideMenuComponentStyle
            {
                SideMenuContainer = new SharpStyle
                {
                    BackgroundColor = "#faf9f8"
                }
            };
        }
    }
}
