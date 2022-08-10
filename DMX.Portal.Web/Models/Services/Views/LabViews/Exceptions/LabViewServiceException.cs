// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Views.LabViews.Exceptions
{
    public class LabViewServiceException : Xeption
    {
        public LabViewServiceException(Xeption innerException)
            : base(message: "Lab service error occurred, contact support.",
                  innerException)
        { }
    }
}
