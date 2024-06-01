using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using EventLocator.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.EventTypes.Edit
{
    public class EditEventTypeViewModel : BaseFormPageViewModel
    {
        #region properties
        private string _label;
        private string _name;
        private string _description;
        private string _iconUrl;
        public Guid Id { get; set; }
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
        public EditEventTypeViewModel(EventType eventType)
        {
            Id = eventType.Id;
            Label = eventType.Label;
            Name = eventType.Name;
            Description = eventType.Description;
            IconUrl = eventType.IconUrl;
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
            EventType editedEventType = new(Id, Label, Name, Description, IconUrl);
            Repository.Instance.EditEventType(editedEventType);
            base.OkCommandExecute();
        }
        #endregion commands
    }
}
