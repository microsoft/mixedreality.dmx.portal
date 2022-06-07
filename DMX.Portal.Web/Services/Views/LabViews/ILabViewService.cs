// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public interface ILabViewService
    {
        ValueTask<List<LabView>> RetrieveAllLabViewsAsync();
    }
}
