using System.Threading.Tasks;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
using Xeptions;

namespace DMX.Portal.Web.Views.Components.NewLabDialogs
{
    public partial class NewLabDialog
    {
        private delegate ValueTask LabViewFunction();

        private async ValueTask TryCatch(LabViewFunction labViewFunction)
        {
            try
            {
                await labViewFunction();
            }
            catch (LabViewValidationException labViewValidationException)
            {
                RenderValidationError(labViewValidationException);
            }
        }

        private void RenderValidationError(Xeption xeption)
        {
            this.Exception = xeption.InnerException;
            this.LabName.Enable();
            this.LabDescription.Enable();
            this.Dialog.EnableButton();
            this.Spinner.Hide();
        }
    }
}
