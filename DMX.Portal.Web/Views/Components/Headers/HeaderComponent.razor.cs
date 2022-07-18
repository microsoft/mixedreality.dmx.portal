// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.Headers;
using DMX.Portal.Web.Views.Bases;

namespace DMX.Portal.Web.Views.Components.Headers
{
    public partial class HeaderComponent
    {
        public HeaderBase Header { get; set; }
        public HeaderStyle Style { get; set; }
        public StyleBase StyleElement { get; set; }
    }
}
