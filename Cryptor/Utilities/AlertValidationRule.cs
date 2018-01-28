﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Cryptor.Model;

namespace Cryptor.Utilities
{
    public class AlertValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            CurrencyAlertData alertData = (value as BindingGroup).Items[0] as CurrencyAlertData;
            if (alertData.UpperBound <= alertData.Price)
            {
                return new ValidationResult(false, "Upper bound should be greater than current price.");
            }
            else if (alertData.LowerBound >= alertData.Price)
            {
                return new ValidationResult(false, "Lower bound should be less than current price.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
