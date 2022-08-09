// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions
{
    public class InvalidLabCommandException : Xeption
    {
        public InvalidLabCommandException()
            : base(message: "Invalid lab command error ocurred, fix the errors and try again.")
        { }
    }
}
