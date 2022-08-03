// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions;

namespace DMX.Portal.Web.Services.Foundations.LabCommands
{
    public partial class LabCommandService
    {
        private static void ValidateLabCommandOnAdd(LabCommand labCommand)
        {
            ValidateIfLabCommandIsNull(labCommand);
        }

        private static void ValidateIfLabCommandIsNull(LabCommand labCommand)
        {
            if (labCommand == null)
            {
                throw new NullLabCommandException();
            }
        }
    }
}
