// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
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
        private delegate ValueTask<LabView> ReturningLabViewFunction();

        private async ValueTask<List<LabView>> TryCatch(ReturningLabViewsFunction returningLabViewsFunction)
        {
            try
            {
                return await returningLabViewsFunction();
            }
            catch (LabDependencyException labDependencyException)
            {
                throw CreateAndLogDependencyException(labDependencyException);
            }
            catch (LabServiceException labServiceException)
            {
                throw CreateAndLogDependencyException(labServiceException);
            }
            catch (Exception exception)
            {
                var failedLabViewServiceException = new FailedLabViewServiceException(exception);
                throw CreateAndLogServiceException(failedLabViewServiceException);
            }
        }

        private async ValueTask<LabView> TryCatch(ReturningLabViewFunction returningLabViewFunction)
        {
            try
            {
                return await returningLabViewFunction();
            }
            catch (NullLabViewException nullLabViewException)
            {
                throw CreateAndLogValidationException(nullLabViewException);
            }
            catch (InvalidLabViewException invalidLabViewException)
            {
                throw CreateAndLogValidationException(invalidLabViewException);
            }
            catch (Exception exception)
            {
                var failedLabViewServiceException = new FailedLabViewServiceException(exception);
                throw CreateAndLogServiceException(failedLabViewServiceException);
            }
        }

        private Exception CreateAndLogValidationException(Xeption xeption)
        {
            var labViewValidationException =
                new LabViewValidationException(xeption);

            this.loggingBroker.LogError(labViewValidationException);

            return labViewValidationException;
        }

        private LabViewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labViewDependencyException =
                new LabViewDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(labViewDependencyException);

            return labViewDependencyException;
        }

        private LabViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labViewServiceException =
                new LabViewServiceException(exception);

            this.loggingBroker.LogError(labViewServiceException);

            return labViewServiceException;
        }
    }
}
