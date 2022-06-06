// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Portal.Web.Models.Views.LabViews.Exceptions
{
    public class FailedLabViewServiceException : Xeption
    {
        public FailedLabViewServiceException(Exception innerException)
            : base(message: "Failed lab service error occurred, contact support.",
                  innerException)
        { }
    }
}
