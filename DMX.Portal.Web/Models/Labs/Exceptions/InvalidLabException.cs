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
        private const string exceptionMessage = "Invalid lab error occurred, please correct the errors and try again.";

        public InvalidLabException()
            : base(message: exceptionMessage)
        { }

        public InvalidLabException(Exception innerException, IDictionary data)
            : base(message: exceptionMessage,
                  innerException, 
                  data)
        { }
    }
}
