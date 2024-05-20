using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Models
{
    public class Tag : Entity
    {
        private string _label;
        private string _color;
        private string _description;

        public Tag(Guid id, string label, string color, string description)
        {
            Id = id;
            Label = label;
            Color = color;
            Description = description;
        }
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }
}
