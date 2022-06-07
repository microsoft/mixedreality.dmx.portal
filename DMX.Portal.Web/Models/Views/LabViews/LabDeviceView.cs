// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

namespace DMX.Portal.Web.Models.Views.LabViews
{
    public class LabDeviceView
    {
        public string Name { get; set; }
        public LabDeviceTypeView Type { get; set; }
        public PowerLevelView PowerLevel { get; set; }
    }
}
