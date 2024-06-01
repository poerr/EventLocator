﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace EventLocator.Validation
{
    public class LetterValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string stringValue = ValidationUtil.GetBoundValue(value) as string;

                foreach(char character in stringValue)
                {
                    if(!char.IsLetter(character) && !char.IsWhiteSpace(character))
                    {
                        return new ValidationResult(false, "Only letters are allowed.");
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
