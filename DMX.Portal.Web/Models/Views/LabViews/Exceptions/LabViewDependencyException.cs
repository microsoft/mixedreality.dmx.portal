// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Portal.Web.Models.Views.LabViews.Exceptions
{
    public class LabViewDependencyException : Xeption
    {
        public LabViewDependencyException(Xeption innerException)
            : base(message: "Lab dependency error occurred - contact support.",
                  innerException)
        { }
    }
}
