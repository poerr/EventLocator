using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Tags.Add
{
    public class AddTagViewModel : BaseDialogViewModel
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

        #region commands
        public override void AddAfterOk()
        {
            base.AddAfterOk();
            Tag newTag = new()
            {
                Id = Guid.NewGuid(),
                Label = Label,
                Color = Color,
                Description = Description
            };
            Repository.Instance.AddTag(newTag);
        }
        #endregion commands
    }
}
