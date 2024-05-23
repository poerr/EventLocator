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
                var stringValue = value as string;
                decimal decimalValue;

                if(decimal.TryParse(stringValue, out decimalValue))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Enter a value numeric value with 2 decimals.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
}
