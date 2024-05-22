using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Tags.Edit
{
    public class EditTagViewModel : BaseDialogViewModel
    {
        #region properties
        private string _label;
        private string _color;
        private string _description;

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
        public EditTagViewModel(Tag tag)
        {
            Id = tag.Id;
            Label = tag.Label;
            Color = tag.Color;
            Description = tag.Description;
        }
        #endregion constructors

        #region commands
        public override void EditAfterOk()
        {
            base.EditAfterOk();
            Tag editedTag = new()
            {
                Id = Id,
                Label = Label,
                Color = Color,
                Description = Description,
            };
            Repository.Instance.EditTag(editedTag);
        }
        #endregion commands
    }
}
