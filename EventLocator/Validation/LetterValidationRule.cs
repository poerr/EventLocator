using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EventLocator.Validation
{
    public class LetterValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string stringValue = value as string;

                if (stringValue.All(char.IsLetter))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Only letters are allowed.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured");
            }
        }
    }
}
