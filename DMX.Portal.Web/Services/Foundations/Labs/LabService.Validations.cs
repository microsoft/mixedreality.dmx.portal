﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using System;
using DMX.Portal.Web.Models.Labs;
using DMX.Portal.Web.Models.Labs.Exceptions;

namespace DMX.Portal.Web.Services.Foundations.Labs
{
    public partial class LabService
    {
        public static void ValidateLabOnAdd(Lab lab)
        {
            ValidateLabIsNotNull(lab);

            Validate(
                (Rule: IsInvalid(lab.Id), Parameter: nameof(Lab.Id)),
                (Rule: IsInvalid(lab.Name), Parameter: nameof(Lab.Name)),
                (Rule: IsInvalid(lab.Description), Parameter: nameof(Lab.Description)),
                (Rule: IsInvalid(lab.Status), Parameter: nameof(lab.Status)));
        }

        private static void ValidateLabIsNotNull(Lab lab)
        {
            if (lab == null)
            {
                throw new NullLabException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(LabStatus labStatus) => new
        {
            Condition = Enum.IsDefined(labStatus) is false,
            Message = "Value is not recognized",
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidLabException = new InvalidLabException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidLabException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidLabException.ThrowIfContainsErrors();
        }
    }
}
