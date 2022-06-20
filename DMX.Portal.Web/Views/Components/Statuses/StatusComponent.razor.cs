// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.StatusComponents;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Components.Statuses
{
    public partial class StatusComponent : ComponentBase
    {
        [Parameter]
        public StatusView Status { get; set; }

        public ImageBase StatusImage { get; set; }
        public string StatusImageUrl { get; set; }
        public StyleBase StyleElement { get; set; }
        public StatusComponentStyle Style { get; set; }

        protected override void OnInitialized()
        {
            SetupStyle();
            this.StatusImageUrl = GetStatusUrl(Status);
        }

        private static string GetStatusUrl(StatusView statusView)
        {
            return statusView switch
            {
                StatusView.Available => "imgs/AvailableStatus.gif",
                StatusView.Offline => "imgs/OfflineStatus.gif",
                _ => "imgs/ReservedStatus.gif"
            };
        }

        private void SetupStyle()
        {
            this.Style = new StatusComponentStyle
            {
                StatusImage = new SharpStyle
                {
                    Height = "15px",
                    Width = "15px",
                    MarginRight = "9px",
                    MarginBottom = "3px"
                }
            };
        }
    }
}
