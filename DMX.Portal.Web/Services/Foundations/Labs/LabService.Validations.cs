using System.Net.NetworkInformation;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public partial class LabService
    {
        public static void ValidateLabOnAdd(Lab lab)
        {
            ValidateLabIsNotNull(lab);
        }

        private static void ValidateLabIsNotNull(Lab lab)
        {
            if (lab == null)
            {
                throw new NullLabException();
            }
        }
    }
}
