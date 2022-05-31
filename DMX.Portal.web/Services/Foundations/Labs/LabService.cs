// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.DmxApis;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Labs;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public partial class LabService : ILabService
    {
        private readonly IDmxApiBroker dmxApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LabService(
            IDmxApiBroker dmxApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.dmxApiBroker = dmxApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<Lab>> RetrieveAllLabsAsync() =>
             await this.dmxApiBroker.GetAllLabsAsync();

    }
}
