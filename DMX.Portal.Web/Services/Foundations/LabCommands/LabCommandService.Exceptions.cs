﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions;
using RESTFulSense.Exceptions;
using Xeptions;

namespace DMX.Portal.Web.Services.Foundations.LabCommands
{
    public partial class LabCommandService
    {
        private delegate ValueTask<LabCommand> ReturningLabCommandFunction();

        private async ValueTask<LabCommand> TryCatch(ReturningLabCommandFunction returningLabCommandFunction)
        {
            try
            {
                return await returningLabCommandFunction();
            }
            catch (NullLabCommandException nullLabCommandException)
            {
                throw CreateAndLogValidationException(nullLabCommandException);
            }
            catch (InvalidLabCommandException invalidLabCommandException)
            {
                throw CreateAndLogValidationException(invalidLabCommandException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedLabCommandDependencyException =
                    new FailedLabCommandDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedLabCommandDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedLabDependencyException =
                    new FailedLabCommandDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedLabDependencyException);
            }
            catch (Exception exception)
            {
                var failedLabServiceException =
                    new FailedLabCommandServiceException(exception);

                throw CreateAndLogServiceException(failedLabServiceException);
            }
        }

        private LabCommandDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var labCommandDependencyException =
                new LabCommandDependencyException(exception);

            this.loggingBroker.LogCritical(labCommandDependencyException);

            throw labCommandDependencyException;
        }

        private LabCommandValidationException CreateAndLogValidationException(Xeption exception)
        {
            var labCommandValidationException =
                new LabCommandValidationException(exception);

            this.loggingBroker.LogError(labCommandValidationException);

            return labCommandValidationException;
        }

        private LabCommandDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var labCommandDependencyException =
                new LabCommandDependencyException(exception);

            this.loggingBroker.LogError(labCommandDependencyException);

            return labCommandDependencyException;
        }

        private LabCommandServiceException CreateAndLogServiceException(Xeption exception)
        {
            var labCommandServiceException =
                new LabCommandServiceException(exception);

            this.loggingBroker.LogError(labCommandServiceException);

            return labCommandServiceException;
        }
    }
}
