﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Services.Views.LabViews;
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
        public TableCellBase DescriptionLabelCell { get; set; }
        public TableCellBase DescriptionValueCell { get; set; }
        public TableCellBase DmxVersionLabelCell { get; set; }
        public TableCellBase DmxVersionValueCell { get; set; }
        public TableCellBase StatusLabelCell { get; set; }
        public TableCellBase StatusValueCell { get; set; }
        public ContainerBase Container { get; set; }
    }
}
