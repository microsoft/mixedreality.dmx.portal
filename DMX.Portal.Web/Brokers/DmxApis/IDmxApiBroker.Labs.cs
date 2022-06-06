// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<List<Lab>> GetAllLabsAsync();
    }
}
