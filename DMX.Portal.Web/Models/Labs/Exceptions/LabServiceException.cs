// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Labs.Exceptions
{
    public class LabServiceException : Xeption
    {
        public LabServiceException(Xeption innerException)
            : base(message: "Lab service error occurred, contact support.",
                  innerException)
        {
        }
    }
}
