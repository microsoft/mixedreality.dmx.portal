// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.LabOverviews
{
    public class LabOverviewStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle LabOverview { get; set; }

        [CssClass]
        public SharpStyle DeviceOverviews { get; set; }

        [CssClass]
        public SharpStyle LabOverviewDetails { get; set; }

        [CssClass]
        public SharpStyle LabOverviewTitle { get; set; }
    }
}