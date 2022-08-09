// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class LabCommandDependencyException : Xeption
    {
        public LabCommandDependencyException(Xeption innerException)
            : base(message: "Lab command dependency error ocurred, contact support.",
                  innerException)
        { }
    }
}
