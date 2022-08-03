// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;

namespace DMX.Portal.Web.Services.Foundations.LabCommands
{
    public partial class LabCommandService : ILabCommandService
    {
        public readonly IDmxApiBroker dmxApiBroker;
        public readonly ILoggingBroker loggingBroker;

        public LabCommandService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<LabCommand> AddLabCommandAsync(LabCommand labCommand) =>
        TryCatch (async () =>
        {
            ValidateLabCommandOnAdd(labCommand);

            return await this.dmxApiBroker.PostLabCommandAsync(labCommand);
        });
    }
}
