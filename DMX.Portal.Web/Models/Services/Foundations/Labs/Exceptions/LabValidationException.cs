// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.Labs.Exceptions
{
    public class LabValidationException : Xeption
    {
        public LabValidationException(Xeption innerException)
            : base(message: "Lab validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
