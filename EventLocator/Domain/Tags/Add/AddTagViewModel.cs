using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using EventLocator.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EventLocator.Domain.Tags.Add
{
    public class AddTagViewModel : BaseFormPageViewModel
    {
        #region properties
        private string _label;
        private string _color;
        private string _description;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value; 
                OnPropertyChanged(nameof(Color));
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
        #endregion properties
        #region constructors
        public AddTagViewModel()
        {
            EntityName = "Tag";
        }
        #endregion constructors
        #region commands
        public override bool CanOkCommandExecute()
        {
            List<string> textInputsToCheck = [Label, Description];
            return ValidationUtil.ValidateTextInputIsOnlyLetters(textInputsToCheck) && 
                ValidationUtil.StringsHaveValue(textInputsToCheck);
        }
        public override void OkCommandExecute()
        {
            Tag newTag = new()
            {
                Id = Guid.NewGuid(),
                Label = Label,
                Color = Color,
                Description = Description
            };
            Repository.Instance.AddTag(newTag);
            base.OkCommandExecute();
        }
        #endregion commands
    }
}
