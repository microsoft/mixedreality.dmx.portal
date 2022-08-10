// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace DMX.Portal.Web.Models.Services.Views.LabViews.Exceptions
{
    public class InvalidLabViewException : Xeption
    {
        public InvalidLabViewException()
            : base(message: "Invalid lab error occurred, please correct the errors and try again.")
        { }

        public InvalidLabViewException(Exception innerException, IDictionary data)
            : base(message: "Invalid lab error occurred, please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}
