// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace DMX.Portal.Web.Models.Configurations
{
    public class DownstreamApiConfiguration
    {
        public string BaseUrl { get; set; }
        public IDictionary<string, string> Scopes { get; set; }
    }
}
