// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Views.LabViews.Exceptions
{
    public class LabViewValidationException : Xeption
    {
        public LabViewValidationException(Xeption innerException)
            : base(message: "Lab validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
