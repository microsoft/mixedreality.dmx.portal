using System;
using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class FailedLabCommandServiceException : Xeption
    {
        public FailedLabCommandServiceException(Exception innerException)
            : base(message: "Failed lab command service error occurred, contact support.",
                   innerException)
        { }
    }
}
