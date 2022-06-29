// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
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
        public TextBoxBase LabId { get; set; }
        public TextBoxBase LabDmxVersion { get; set; }
        public TextBoxBase LabName { get; set; }
        public TextBoxBase LabDescription { get; set; }
        public bool IsVisible { get; set; }
        public LabView LabView { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
        public SpinnerBase Spinner { get; set; }
        public ValidationSummaryBase LabNameValidationSummary { get; set; }
        public ValidationSummaryBase LabDescriptionValidationSummary { get; set; }
        public ContainerBase LabIdContainer { get; set; }
        public ContainerBase LabDmxVersionContainer { get; set; }
        public ContainerBase LabNameContainer { get; set; }
        public ContainerBase LabDescriptionContainer { get; set; }

        private string labIdString;

        protected override void OnInitialized()
        {
            this.LabView = new LabView();
            this.State = NewLabDialogComponentState.Content;
            labIdString = this.LabView.Id.ToString();
        }

        public void OpenDialog()
        {
            this.Dialog.Show();
            this.IsVisible = this.Dialog.IsVisible;
            this.LabView.DmxVersion = "1.0";
            this.LabView.Id = Guid.NewGuid();
            labIdString = this.LabView.Id.ToString();

            InvokeAsync(StateHasChanged);
        }

        public ValueTask AddLabViewAsync() =>
            TryCatch(async () =>
            {
                this.LabName.Disable();
                this.LabDescription.Disable();
                this.Dialog.DisableButton();
                this.Spinner.Show();

                await this.LabViewService.AddLabViewAsync(this.LabView);

                CloseDialog();
            });

        public void CloseDialog()
        {
            this.Dialog.Hide();
            this.IsVisible = this.Dialog.IsVisible;
        }
    }
}
