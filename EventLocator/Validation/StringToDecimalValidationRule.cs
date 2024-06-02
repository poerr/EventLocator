using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EventLocator.Validation
{
    public class StringToDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var stringValue = ValidationUtil.GetBoundValue(value) as string;
                decimal decimalValue;

                if (!string.IsNullOrEmpty(stringValue))
                {
                    string[] parts = stringValue.Split(".");
                    if (parts.Length != 2)
                    {
                        return new ValidationResult(false, "Enter a numeric value with 2 decimals.");
                    }
                    foreach (string part in parts)
                    {
                        foreach (char character in part)
                        {
                            if (!char.IsNumber(character))
                            {
                                return new ValidationResult(false, "Enter a numeric value with 2 decimals.");
                            }
                        }
                    }
                    if (parts[1].Length != 2)
                    {
                        return new ValidationResult(false, "Enter a numeric value with 2 decimals.");
                    }
                }
                if (decimal.TryParse(stringValue, out decimalValue))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Enter a numeric value with 2 decimals.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
