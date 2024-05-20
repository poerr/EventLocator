using EventLocator.Common;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Events.Edit
{
    public class EditEventViewModel : BaseDialogViewModel
    {
        private string _label;
        private string _name;
        private string _description;
        private int? _eventTypeId;
        private Attendance _attendance;
        private string? _iconUrl;
        private bool _isCharity;
        private decimal _averageHostingExpenses;
        private string _country;
        private string _city;
        private DateTime[] _previousEventDates;
        private DateTime _eventDate;
        //public EditEventViewModel(Event selectedEvent)
        //{
        //    setProperties(selectedEvent);
        //}

        //private void setProperties(Event selectedEvent)
        //{
        //    Label = selectedEvent.Label;
        //    Name = selectedEvent.Name;
        //    Description = selectedEvent.Description;
        //    EventTypeId = selectedEvent.Type?.Id;
        //    Attendance = selectedEvent.Attendance;
        //    IconUrl = selectedEvent.IconUrl;
        //    IsCharity = selectedEvent.IsCharity;
        //    AverageHostingExpenses = selectedEvent.AverageHostingExpenses;
        //    Country = selectedEvent.Country;
        //    City = selectedEvent.City;
        //    //PreviousEventDates = selectedEvent.PreviousEventDates;
        //    EventDate = selectedEvent.EventDate;
        //}
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

        public int? EventTypeId
        {
            get
            {
                return _eventTypeId;
            }
            set
            {
                _eventTypeId = value;
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

        public DateTime[] PreviousEventDates
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
    }
}
