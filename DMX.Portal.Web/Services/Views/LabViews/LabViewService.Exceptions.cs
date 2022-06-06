using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs.Exceptions;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;
using Xeptions;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public partial class LabViewService
    {
        private delegate ValueTask<List<LabView>> ReturningLabViewsFunction();

        private async ValueTask<List<LabView>> TryCatch(ReturningLabViewsFunction returningLabViewsFunction)
        {
            try
            {
                return await returningLabViewsFunction();
            }
            catch (LabDependencyException labDependencyException)
            {
                var labViewDependencyException = new LabViewDependencyException(labDependencyException.InnerException as Xeption);
                this.loggingBroker.LogError(labViewDependencyException);

                throw labViewDependencyException;
            }
            catch (LabServiceException labServiceException)
            {
                var labViewDependencyException = new LabViewDependencyException(labServiceException.InnerException as Xeption);
                this.loggingBroker.LogError(labViewDependencyException);

                throw labViewDependencyException;
            }
        }
    }
}
