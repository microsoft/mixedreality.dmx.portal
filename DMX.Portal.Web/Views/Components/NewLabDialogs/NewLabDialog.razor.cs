// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using DMX.Portal.Web.Models.Views.Components.NewLabDialogComponents;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Services.Views.LabViews;
using DMX.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;

namespace DMX.Portal.Web.Views.Components.NewLabDialogs
{
    public partial class NewLabDialog : ComponentBase
    {
        [Inject]
        public ILabViewService LabViewService { get; set; }

        public NewLabDialogComponentState State { get; set; }
        public DialogBase Dialog { get; set; }
        public TextBoxBase LabName { get; set; }
        public TextBoxBase LabDescription { get; set; }
        public bool IsVisible { get; set; }
        public LabView LabView { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public SpinnerBase Spinner { get; set; }
        public ValidationSummaryBase ContentValidationSummary { get; set; }
    }
}
