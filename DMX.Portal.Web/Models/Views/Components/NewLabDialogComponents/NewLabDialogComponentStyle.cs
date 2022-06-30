// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents
{
    public class NewLabDialogComponentStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle NewLabTextBox { get; set; }

        [CssClass]
        public SharpStyle NewLabErrorMessage { get; set; }
    }
}
