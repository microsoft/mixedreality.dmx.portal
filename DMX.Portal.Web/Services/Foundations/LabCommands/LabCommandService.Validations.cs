// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using DMX.Portal.Web.Models.Services.Foundations.LabCommands;
using DMX.Portal.Web.Models.Services.Foundations.LabCommands.Exceptions;
using DMX.Portal.Web.Models.Services.Foundations.Labs.Exceptions;
using Syncfusion.Blazor.Charts.Chart.Internal;
using System;

namespace DMX.Portal.Web.Services.Foundations.LabCommands
{
    public partial class LabCommandService
    {
        public static void ValidateLabCommandOnAdd(LabCommand labCommand)
        {
            ValidateLabCommandIsNotNull(labCommand);

            Validate(
                (Rule: IsInvalidId(labCommand.Id), Parameter: nameof(LabCommand.Id)),
                (Rule: IsInvalidId(labCommand.LabId), Parameter: nameof(labCommand.LabId)),
                (Rule: IsInvalidDate(labCommand.CreatedDate), Parameter: nameof(LabCommand.CreatedDate)),
                (Rule: IsInvalidDate(labCommand.UpdatedDate), Parameter: nameof(LabCommand.UpdatedDate)));
        }

        private static bool IsInvalidDate(DateTimeOffset createdDate) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static bool IsInvalidId(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static void ValidateLabCommandIsNotNull(LabCommand labCommand)
        {
            if (labCommand is null)
            {
                throw new NullLabCommandException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLabCommandException = new InvalidLabCommandException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLabCommandException.UpsertDataList(
                        key: parameter,
                        value: rule.message);
                }
            }

            invalidLabCommandException.ThrowIfContainsErrors();
        }
    }
}
