// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DMX.Portal.Web.Models.Views.LabViews
{
    public class LabView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DmxVersion { get; set; }
        public LabStatusView Status { get; set; }
        public List<LabDeviceView> Devices { get; set; }
    }
}
