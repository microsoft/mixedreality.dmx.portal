// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.Menus
{
    public partial class SideMenuComponent : ComponentBase
    {
        public ContainerBase Container { get; set; }
        public LeftSideMenuHeaderBase TitleContainer { get; set; }

        protected override void OnInitialized()
        {
            SetupStyle();
        }
    }
}
