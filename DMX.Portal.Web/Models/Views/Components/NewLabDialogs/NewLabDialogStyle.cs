// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents
{
    public class NewLabDialogStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle NewLabTextbox { get; set; }

        [CssClass]
        public SharpStyle NewLabErrorMessage { get; set; }
    }
}
