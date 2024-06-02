using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EventLocator.Validation
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string stringValue = ValidationUtil.GetBoundValue(value).ToString() as string;
                DateTime date;

                if(DateTime.TryParse(stringValue, out date))
                {
                   if(date <= DateTime.Now)
                   {
                        return new ValidationResult(false, "Date must be in the future.");
                   }
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured");
            }
        }
    }
}
