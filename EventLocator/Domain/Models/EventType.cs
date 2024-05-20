using EventLocator.Domain.EventTypes.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace EventLocator.Domain.Models
{
    public class EventType : Entity
    {
        private string _label;
        private string _name;
        private string _description;
        private string _iconUrl;

        private BitmapImage _iconBitmap;
        public EventType(Guid id, string label, string name, string description, string iconUrl)
        {
            Id = id;
            Label = label;  
            Name = name;
            Description = description;
            IconUrl = iconUrl;
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

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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

        public string IconUrl
        {
            get
            {
                return _iconUrl;
            }
            set
            {
                _iconUrl = value;
                OnPropertyChanged(nameof(IconUrl));
            }
        }
    }
}
