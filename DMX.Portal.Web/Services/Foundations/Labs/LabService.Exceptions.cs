// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public partial class LabService
    {
        private delegate ValueTask<List<Lab>> ReturningLabsFunction();

        private async ValueTask<List<Lab>> TryCatch(ReturningLabsFunction returningLabsFunction)
        {
            try
            {
                return await returningLabsFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw this.CreateAndLogCriticalDependencyException(failedLabDependencyException);

            } 
            catch (HttpResponseUnauthorizedException httpResponseUrlNotFoundException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseUrlNotFoundException);

                throw this.CreateAndLogCriticalDependencyException(failedLabDependencyException);
            } 
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabDependencyException = new FailedLabDependencyException(httpResponseForbiddenException);

                throw this.CreateAndLogCriticalDependencyException(failedLabDependencyException);
            } 
        }


        private LabDependencyException CreateAndLogCriticalDependencyException(Xeption xeption)
        {
            var labDependencyException = new LabDependencyException(xeption);
            this.loggingBroker.LogCritical(labDependencyException);

            return labDependencyException;
        }

    }
}
