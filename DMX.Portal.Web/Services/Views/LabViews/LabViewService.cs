// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Brokers.Loggings;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Foundations.Labs;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public partial class LabViewService : ILabViewService
    {
        private readonly ILabService labService;
        private readonly ILoggingBroker loggingBroker;

        public LabViewService(
            ILabService labService,
            ILoggingBroker loggingBroker)
        {
            this.labService = labService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<List<LabView>> RetrieveAllLabViewsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
