// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;
using Syncfusion.Blazor.Diagram;

namespace DMX.Portal.Web.Models.Views.Components.LabOverviews
{
    public class LabOverviewStyle : SharpStyle
    {
        [CssClass]
        public ShapeStyle LabOverview { get; set; }
    }
}
