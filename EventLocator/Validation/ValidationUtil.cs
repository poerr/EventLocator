using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EventLocator.Validation
{
    public class ValidationUtil
    {
        public static bool ValidateTextInputIsOnlyLetters(List<string> textInputs)
        {
            foreach (string textInput in textInputs)
            {
                if (textInput != null)
                {
                    foreach(char character in textInput)
                    {
                        if(!char.IsLetter(character) && !char.IsWhiteSpace(character))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AreDatesInPast(List<DateTime> datesToCheck)
        {
            foreach(DateTime dateToCheck in datesToCheck)
            {
                if(dateToCheck >= DateTime.Now)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsDateInFuture(DateTime dateToCheck) 
        {
            return dateToCheck > DateTime.Now;
        }
        public static bool StringsHaveValue(List<string> textInputs)
        {
            foreach (string textInput in textInputs)
            {
                if(string.IsNullOrEmpty(textInput) || textInput == " ")
                {
                    return false;
                }
            }
            return true;
        }
        public static bool InputHasValue(object inputValue)
        {
            if(inputValue != default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static object GetBoundValue(object value)
        {
            if (value is BindingExpression)
            {
                BindingExpression binding = (BindingExpression)value;

                object dataItem = binding.DataItem;
                string propertyName = binding.ParentBinding.Path.Path;

                object propertyValue = dataItem.GetType().GetProperty(propertyName).GetValue(dataItem, null);

                return propertyValue;
            }
            else
            {
                return value;
            }
        }
    }
}
