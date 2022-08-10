// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Services.Views.LabViews.Exceptions
{
    public class NullLabViewException : Xeption
    {
        public NullLabViewException()
            : base(message: "Lab is null.")
        { }
    }
}
