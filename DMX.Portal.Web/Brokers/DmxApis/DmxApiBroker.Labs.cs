// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync() =>
            await GetAsync<List<Lab>>(LabsRelativeUrl);
    }
}
