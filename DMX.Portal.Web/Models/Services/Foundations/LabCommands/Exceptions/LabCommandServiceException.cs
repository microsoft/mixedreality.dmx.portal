using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class LabCommandServiceException : Xeption
    {
        public LabCommandServiceException(Xeption innerException)
            : base(message: "Lab command service error ocurred, contact support.",
                   innerException)
        { }
    }
}
