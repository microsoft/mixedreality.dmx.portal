// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using System.Threading.Tasks;

namespace DMX.Portal.Web.Services.Foundations.LabCommands
{
    public interface ILabCommandService
    {
        ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand);
    }
}
