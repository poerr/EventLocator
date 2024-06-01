using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EventLocator.Validation
{
    public class RequiredTextInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string stringValue = ValidationUtil.GetBoundValue(value) as string;

                if(string.IsNullOrEmpty(stringValue) || stringValue == " ")
                {
                    return new ValidationResult(false, "This text field is required.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured");
            }
        }
    }
}
