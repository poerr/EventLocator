using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Events.Add;
using EventLocator.Domain.Events.Edit;
using EventLocator.Domain.Models;
using EventLocator.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EventLocator.Domain.Events.Index
{
    public class IndexEventViewModel : BasePageViewModel<Event>
    {
        #region properties
        private string _searchedLabel = "";
        private string _searchedName = "";
        private string _searchedDescription = "";
        private ComboBoxData<EventType> _searchedEventType;
        private ComboBoxData<Attendance> _searchedAttendance;
        private bool _searchedIsCharity;
        private string _searchedExpensesFrom = "";
        private string _searchedExpensesTo = "";
        private string _searchedCountry = "";
        private string _searchedCity = "";
        private DateTime _searchedEventDateFrom = DateTime.Today;
        private DateTime _searchedEventDateTo = DateTime.Today;

        private List<ComboBoxData<EventType>> _eventTypeComboBoxOptions = [];
        private List<ComboBoxData<Attendance>> _attendanceComboBoxOptions = [];

        public string SearchedLabel
        {
            get { return _searchedLabel; }
            set
            {
                _searchedLabel = value;
                OnPropertyChanged(nameof(SearchedLabel));
            }
        }
        public string SearchedName
        {
            get { return _searchedName; }
            set
            {
                _searchedName = value;
                OnPropertyChanged(nameof(SearchedName));
            }
        }
        public string SearchedDescription
        {
            get { return _searchedDescription; }
            set
            {
                _searchedDescription = value;
                OnPropertyChanged(nameof(SearchedDescription));
            }
        }
        public ComboBoxData<EventType> SearchedEventType
        {
            get { return _searchedEventType; }
            set
            {
                _searchedEventType = value;
                OnPropertyChanged(nameof(SearchedEventType));
            }
        }
        public ComboBoxData<Attendance> SearchedAttendance
        {
            get { return _searchedAttendance; }
            set
            {
                _searchedAttendance = value;
                OnPropertyChanged(nameof(SearchedAttendance));
            }
        }
        public bool SearchedIsCharity
        {
            get { return _searchedIsCharity; }
            set
            {
                _searchedIsCharity = value;
                OnPropertyChanged(nameof(SearchedIsCharity));
            }
        }
        public string SearchedExpensesFrom
        {
            get { return _searchedExpensesFrom; }
            set
            {
                _searchedExpensesFrom = value;
                OnPropertyChanged(nameof(SearchedExpensesFrom));
            }
        }
        public string SearchedExpensesTo
        {
            get { return _searchedExpensesTo; }
            set
            {
                _searchedExpensesTo = value;
                OnPropertyChanged(nameof(SearchedExpensesTo));
            }
        }
        public string SearchedCountry
        {
            get { return _searchedCountry; }
            set
            {
                _searchedCountry = value;
                OnPropertyChanged(nameof(SearchedCountry));
            }
        }
        public string SearchedCity
        {
            get { return _searchedCity; }
            set
            {
                _searchedCity = value;
                OnPropertyChanged(nameof(SearchedCity));
            }
        }
        public DateTime SearchedEventDateFrom
        {
            get { return _searchedEventDateFrom; }
            set
            {
                _searchedEventDateFrom = value;
                OnPropertyChanged(nameof(SearchedEventDateFrom));
            }
        }
        public DateTime SearchedEventDateTo
        {
            get { return _searchedEventDateTo; }
            set
            {
                _searchedEventDateTo = value;
                OnPropertyChanged(nameof(SearchedEventDateTo));
            }
        }
        public List<ComboBoxData<EventType>> EventTypeComboBoxOptions
        {
            get { return _eventTypeComboBoxOptions; }
            set
            {
                _eventTypeComboBoxOptions = value;
                OnPropertyChanged(nameof(EventTypeComboBoxOptions));
            }
        }
        public List<ComboBoxData<Attendance>> AttendanceComboBoxOptions
        {
            get { return _attendanceComboBoxOptions; }
            set
            {
                _attendanceComboBoxOptions = value;
                OnPropertyChanged(nameof(AttendanceComboBoxOptions));
            }
        }
        #endregion properties
        #region constructors
        public IndexEventViewModel() 
        {            
            EventTypeComboBoxOptions = Repository.Instance.eventTypeDropdownOptions();
            AttendanceComboBoxOptions = Repository.Instance.attendanceDropdownOptions();

            LoadTableData();
            FilterEntities();
        }
        #endregion constructors
        #region commands
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            NavigateToPage("Add", "Event", null);
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            NavigateToPage("Edit", "Event", SelectedEntity);
        }
        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
        }
        public override void DeleteAfterOk(Event item)
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteEvent(item.Id);
            LoadTableData();
        }
        public override bool CanSearchCommandExecute()
        {
            return ValidationUtil.ValidateTextInputIsOnlyLetters([SearchedLabel, SearchedDescription, SearchedName, SearchedCountry, SearchedCity]) &&
                ValidationUtil.DecimalValueValidation([SearchedExpensesFrom, SearchedExpensesTo]);
        }
        public override void SearchCommandExecute()
        {
            base.SearchCommandExecute();
            if(!string.IsNullOrEmpty(SearchedLabel))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Label.ToLower().Contains(SearchedLabel.ToLower())));
            }

            if(!string.IsNullOrEmpty(SearchedName))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Name.ToLower().Contains(SearchedName.ToLower())));
            }

            if(!string.IsNullOrEmpty(SearchedDescription))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Description.ToLower().Contains(SearchedDescription.ToLower())));
            }

            if(SearchedEventType != null)
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Type.Id == SearchedEventType.Value.Id));
            }

            if(SearchedAttendance != null)
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Attendance == SearchedAttendance.Value));
            }

            if(!string.IsNullOrEmpty(SearchedExpensesFrom))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.AverageHostingExpenses >= decimal.Parse(SearchedExpensesFrom)));
            }

            if(!string.IsNullOrEmpty(SearchedExpensesTo))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.AverageHostingExpenses <= decimal.Parse(SearchedExpensesTo)));
            }

            if(!string.IsNullOrEmpty(SearchedCity))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.City.ToLower().Contains(SearchedCity.ToLower())));
            }

            if (!string.IsNullOrEmpty(SearchedCountry))
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.Country.ToLower().Contains(SearchedCountry.ToLower())));
            }

            if(SearchedEventDateFrom != default)
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.EventDate >= SearchedEventDateFrom));
            }

            if (SearchedEventDateTo != default)
            {
                SearchedEntities = new ObservableCollection<Event>(SearchedEntities.Where(entity => entity.EventDate >= SearchedEventDateTo));
            }
        }
        public override void ClearSearchCommandExecute()
        {
            base.ClearSearchCommandExecute();
        }
        public override void DetailsCommandExecute()
        {
            base.DetailsCommandExecute();
            NavigateToPage("Details", "Event", SelectedEntity);
        }
        #endregion commands
        #region functions
        public override void LoadTableData()
        {
            Entities = new ObservableCollection<Event>(Repository.Instance.GetAllEvents());
            SearchedEntities = Entities;
        }
        public override void FilterEntities()
        {
            base.FilterEntities();

            if(!string.IsNullOrEmpty(Filter))
            {
                string filter = Filter.ToLower();

                SearchedEntities = new ObservableCollection<Event>(Entities.Where(
                    entity =>
                    entity.Label.ToLower().Contains(filter) ||
                    entity.Name.ToLower().Contains(filter) ||
                    entity.Description.ToLower().Contains(filter) ||
                    Enum.GetName(typeof(Attendance), entity.Attendance).ToLower().Contains(filter) ||
                    entity.AverageHostingExpenses.ToString().ToLower().Contains(filter) ||
                    entity.Country.ToLower().Contains(filter) ||
                    entity.City.ToLower().Contains(filter)
                ));
            }
            else
            {
                SearchedEntities = Entities;
            }
        }
        public override void ClearSearchFields()
        {
            SearchedLabel = string.Empty;
            SearchedName = string.Empty;
            SearchedDescription = string.Empty;
            SearchedEventType = default;
            SearchedAttendance = default;
            SearchedExpensesFrom = string.Empty;
            SearchedExpensesTo = string.Empty;
            SearchedCity = string.Empty;
            SearchedCountry = string.Empty;
            SearchedEventDateFrom = DateTime.Now;
            SearchedEventDateTo = DateTime.Now;
        }
        #endregion functions
    }
}
