// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial interface IDmxApiBroker
    {
        ValueTask<LabCommand> PostLabCommandAsync(LabCommand labCommand);
        ValueTask<List<LabCommand>> GetAllLabCommandsAsync();
    }
}
