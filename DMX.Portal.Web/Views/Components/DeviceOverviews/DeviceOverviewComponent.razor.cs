﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Services.Views.LabViews;
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
        public string PowerLevelImageUrl { get; set; }
        public LabelBase DeviceLabel { get; set; }
        public ContainerBase Container { get; set; }

        protected override void OnInitialized()
        {
            this.ImageUrl = RetrieveImageUrl(Device.Type);
            this.PowerLevelImageUrl = RetrievePowerLeverUrl(Device.PowerLevel);
        }

        private static string RetrieveImageUrl(LabDeviceTypeView labDeviceTypeView)
        {
            return labDeviceTypeView switch
            {
                LabDeviceTypeView.PC => "imgs/NUC.png",
                LabDeviceTypeView.HeadMountedDisplay => "imgs/HoloLens.png",
                LabDeviceTypeView.Phone => "imgs/Phone.png",
                _ => "imgs/OtherDevice.png"
            };
        }

        private static string RetrievePowerLeverUrl(PowerLevelView powerLevelView)
        {
            return powerLevelView switch
            {
                PowerLevelView.Full => "imgs/FullPowerLevel.png",
                PowerLevelView.High => "imgs/HighPowerLevel.png",
                PowerLevelView.Medium => "imgs/MediumPowerLevel.png",
                PowerLevelView.Low => "imgs/LowPowerLevel.png",
                PowerLevelView.Empty => "imgs/EmptyPowerLevel.png",
                _ => ""
            };
        }
    }
}
