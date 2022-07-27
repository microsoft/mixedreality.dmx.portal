// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.Labs;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public interface ILabService
    {
        ValueTask<Lab> AddLabAsync(Lab lab);
        ValueTask<List<Lab>> RetrieveAllLabsAsync();
    }
}
