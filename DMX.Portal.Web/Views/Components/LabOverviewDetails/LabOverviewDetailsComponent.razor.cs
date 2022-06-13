// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.LabOverviewDetails
{
    public partial class LabOverviewDetailsComponent : ComponentBase
    {
        [Parameter]
        public LabView Lab { get; set; }

        public TableBase DetailsTable { get; set; }

        public TableCellBase NameLabelCell { get; set; }

        public TableCellBase NameValueCell { get; set; }
    }
}
