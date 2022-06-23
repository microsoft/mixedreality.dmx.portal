// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Portal.Web.Models.Labs.Exceptions
{
    public class InvalidLabException : Xeption
    {
        public InvalidLabException()
            : base(message: "Invalid lab error occurred, please correct the errors and try again.")
        { }

        public InvalidLabException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab error occurred, please correct the errors and try again.",
                  innerException, 
                  data)
        { }
    }
}
