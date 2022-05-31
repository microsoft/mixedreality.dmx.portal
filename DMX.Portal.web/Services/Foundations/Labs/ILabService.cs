// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public interface ILabService
    {
        ValueTask<List<Lab>> RetrieveAllLabsAsync();
    }
}
