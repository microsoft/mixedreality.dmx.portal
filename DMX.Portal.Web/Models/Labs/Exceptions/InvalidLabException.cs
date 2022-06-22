// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Labs.Exceptions
{
    public class InvalidLabException : Xeption
    {
        public InvalidLabException(Xeption innerException)
            : base(message: "Invalid lab error occurred, please contact support.",
                  innerException)
        { }
    }
}
