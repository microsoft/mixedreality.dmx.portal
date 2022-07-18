// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.Headers;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Components.Headers
{
    public partial class HeaderComponent : ComponentBase
    {
        public HeaderBase Header { get; set; }
        public HeaderStyle Style { get; set; }
        public StyleBase StyleElement { get; set; }

        protected override void OnInitialized() =>
            SetupStyles();

        private void SetupStyles()
        {
            this.Style = new HeaderStyle
            {
                TopRow = new SharpStyle
                {
                    BackgroundColor = "#0078d4",
                    BorderBottom = "1px solid #d6d5d5",
                    JustifyContent = "flex-end",
                    Height = "3.5rem",
                    Display = "flex",
                    AlignItems = "center",
                }
            };
        }
    }
}
