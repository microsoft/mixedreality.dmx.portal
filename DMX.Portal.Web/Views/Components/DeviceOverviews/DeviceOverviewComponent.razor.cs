// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponent : ComponentBase
    {
        [Parameter]
        public LabDeviceView Device { get; set; }

        public ImageBase Image { get; set; }
        public ImageBase PowerLevelImage { get; set; }
        public string ImageUrl { get; set; }
        public LabelBase DeviceLabel { get; set; }

        protected override void OnInitialized() =>
            this.ImageUrl = RetrieveImageUrl(Device.Type);

        private static string RetrieveImageUrl(LabDeviceTypeView labDeviceTypeView)
        {
            return labDeviceTypeView switch
            {
                LabDeviceTypeView.PC => "imgs/NUC.png",
                LabDeviceTypeView.HeadMountedDisplay => "imgs/HoloLens.png",
                LabDeviceTypeView.Phone => "imgs/Phone.png",
                _ => ""
            };
        }
    }
}
