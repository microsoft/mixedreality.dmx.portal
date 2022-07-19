// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents;
using DMX.Portal.Web.Views.Bases;
using SharpStyles.Models;

namespace DMX.Portal.Web.Views.Components.NewLabDialogs
{
    public partial class NewLabDialog
    {
        public StyleBase StyleElement { get; set; }
        public NewLabDialogStyle Style { get; set; }

        private void SetupStyle()
        {
            this.Style = new NewLabDialogStyle
            {
                NewLabTextbox = new SharpStyle
                {
                    PaddingBottom = "20px"
                },

                NewLabErrorMessage = new SharpStyle
                {
                    Color = "red"
                }
            };
        }
    }
}
