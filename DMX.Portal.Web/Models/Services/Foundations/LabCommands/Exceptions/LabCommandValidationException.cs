// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class LabCommandValidationException : Xeption
    {
        public LabCommandValidationException(Xeption innerException)
            : base(message: "Lab command validation error occured. Please fix and try again.",
                  innerException)
        { }
    }
}
