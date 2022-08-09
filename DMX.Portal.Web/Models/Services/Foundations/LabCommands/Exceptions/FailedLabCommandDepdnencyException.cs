// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class FailedLabCommandDependencyException : Xeption
    {
        public FailedLabCommandDependencyException(Exception innerException)
            : base(message: "Failed lab command dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
