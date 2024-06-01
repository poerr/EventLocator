using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using EventLocator.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EventLocator.Domain.EventTypes.Add
{
    public class AddEventTypeViewModel : BaseFormPageViewModel
    {
        #region properties
        public string _label;
        public string _name;
        public string _description;
        public string _iconUrl;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public string IconUrl
        {
            get { return _iconUrl; }
            set
            {
                _iconUrl = value;
                OnPropertyChanged(nameof(IconUrl));
            }
        }
        #endregion properties
        #region constructors
        public AddEventTypeViewModel()
        {
            EntityName = "EventType";
        }
        #endregion constructors
        #region commands
        public override bool CanOkCommandExecute()
        {
            List<string> textInputsToCheck = [Label, Name, Description];
            return ValidationUtil.ValidateTextInputIsOnlyLetters(textInputsToCheck) &&
                ValidationUtil.StringsHaveValue(textInputsToCheck) &&
                !string.IsNullOrEmpty(IconUrl);
        }
        public override void OkCommandExecute()
        {
            EventType newEventType = new(Guid.NewGuid(), Label, Name, Description, IconUrl);
            Repository.Instance.AddEventType(newEventType);
            base.OkCommandExecute();
        }
        #endregion commands
    }
}
