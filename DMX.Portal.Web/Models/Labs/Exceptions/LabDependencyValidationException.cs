using Xeptions;

namespace DMX.Portal.Web.Models.Labs.Exceptions
{
    public class LabDependencyValidationException : Xeption
    {
        public LabDependencyValidationException(Xeption exception)
            : base(message: "Lab validation dependency error occurred, please try again.",
                  innerException: exception)
        { }
    }
}
