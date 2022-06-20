// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using Tynamix.ObjectFiller;

namespace DMX.Portal.Web.Brokers.DmxApis
{
    public partial class DmxApiBroker
    {
        private const string LabsRelativeUrl = "api/labs";

        public async ValueTask<List<Lab>> GetAllLabsAsync() =>
            await ValueTask.FromResult(CreateRandomLabs());

        private List<Lab> CreateRandomLabs()
        {
            var filler = new Filler<List<Lab>>();

            filler.Setup()
                .ListItemCount(3);

            return filler.Create();
        }
    }
}
