using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public partial class LabViewService
    {
        public static void ValidateLabViewOnAdd(LabView labView)
        {
            ValidateLabViewIsNotNull(labView);
        }

        private static void ValidateLabViewIsNotNull(LabView labView)
        {
            if (labView is null)
            {
                throw new NullLabViewException();
            }
        }
    }
}
