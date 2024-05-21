using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Models
{
    public class Event : Entity
    {
        private string _label;
        private string _name;
        private string _description;
        private Guid _eventTypeId;
        private EventType? _type;
        private Attendance _attendance;
        private string? _iconUrl;
        private bool _isCharity;
        private decimal _averageHostingExpenses;
        private string _country;
        private string _city;
        private List<DateTime> _previousEventDates;
        private DateTime _eventDate;
        private List<Tag> _tags;
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
        public Guid EventTypeId
        {
            get { return _eventTypeId; }
            set
            {
                _eventTypeId = value;
                OnPropertyChanged(nameof(EventTypeId));
            }
        }
        
        public EventType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public Attendance Attendance
        {
            get
            {
                return _attendance;
            }
            set
            {
                _attendance = value;
                OnPropertyChanged(nameof(Attendance));
            }
        }

        public string? IconUrl
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

        public bool IsCharity
        {
            get
            {
                return _isCharity;
            }
            set
            {
                _isCharity = value;
                OnPropertyChanged(nameof(IsCharity));
            }
        }

        public decimal AverageHostingExpenses
        {
            get
            {
                return _averageHostingExpenses;
            }
            set
            {
                _averageHostingExpenses = value;
                OnPropertyChanged(nameof(AverageHostingExpenses));
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        [Column(TypeName = "timestamp without time zone[]")]
        public List<DateTime> PreviousEventDates
        {
            get
            {
                return _previousEventDates;
            }
            set
            {
                _previousEventDates = value;
                OnPropertyChanged(nameof(PreviousEventDates));
            }
        }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }
            set
            {
                _eventDate = value;
                OnPropertyChanged(nameof(EventDate));
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
    }
}
