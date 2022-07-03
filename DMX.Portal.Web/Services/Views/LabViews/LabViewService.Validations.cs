// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Models.Views.LabViews.Exceptions;

namespace DMX.Portal.Web.Services.Views.LabViews
{
    public partial class LabViewService
    {
        public static void ValidateLabViewOnAdd(LabView labView)
        {
            ValidateLabViewIsNotNull(labView);

            Validate(
                (Rule: IsInvalid(labView.Id), Parameter: nameof(LabView.Id)),
                (Rule: IsInvalidId(labView.ExternalId), Parameter: nameof(LabView.ExternalId)),
                (Rule: IsInvalid(labView.Name), Parameter: nameof(LabView.Name)),
                (Rule: IsInvalid(labView.Description), Parameter: nameof(LabView.Description)),
                (Rule: IsInvalid(labView.DmxVersion), Parameter: nameof(LabView.DmxVersion)));
        }

        private static void ValidateLabViewIsNotNull(LabView labView)
        {
            if (labView is null)
            {
                throw new NullLabViewException();
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidId(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Id is required"
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = Guid.Empty == id,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLabViewException = new InvalidLabViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLabViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLabViewException.ThrowIfContainsErrors();
        }
    }
}
