using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using EventLocator.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Events.Edit
{
    public class EditEventViewModel : BaseFormPageViewModel
    {
        #region properties
        private Guid _id;
        private string _label;
        private string _name;
        private string _description;
        private ComboBoxData<EventType> _eventType;
        private ComboBoxData<Attendance> _attendance;
        private string _iconUrl;
        private bool _isCharity;
        private string _averageHostingExpenses;
        private string _country;
        private string _city;
        private DateTime _eventDate;
        private ObservableCollection<Tag> _tags = [];
        private ObservableCollection<DateTime> _previousEventDates = [];
        private ComboBoxData<Tag> _selectedTag;
        private DateTime _selectedPreviousDate;
        private Tag _tagToRemove;
        private DateTime _previousDateToRemove = default;
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
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
        public ComboBoxData<EventType> EventType
        {
            get { return _eventType; }
            set
            {
                _eventType = value;
                OnPropertyChanged(nameof(EventType));
            }
        }
        public ComboBoxData<Attendance> Attendance
        {
            get { return _attendance; }
            set
            {
                _attendance = value;
                OnPropertyChanged(nameof(Attendance));
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
        public bool IsCharity
        {
            get { return _isCharity; }
            set
            {
                _isCharity = value;
                OnPropertyChanged(nameof(IsCharity));
            }
        }
        public string AverageHostingExpenses
        {
            get { return _averageHostingExpenses; }
            set
            {
                _averageHostingExpenses = value;
                OnPropertyChanged(nameof(AverageHostingExpenses));
            }
        }
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public DateTime EventDate
        {
            get { return _eventDate; }
            set
            {
                _eventDate = value;
                OnPropertyChanged(nameof(EventDate));
            }
        }
        public ObservableCollection<Tag> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }
        public ObservableCollection<DateTime> PreviousEventDates
        {
            get { return _previousEventDates; }
            set
            {
                _previousEventDates = value;
                OnPropertyChanged(nameof(PreviousEventDates));
            }
        }
        public ComboBoxData<Tag> SelectedTag
        {
            get { return _selectedTag; }
            set
            {
                _selectedTag = value;
                OnPropertyChanged(nameof(SelectedTag));
            }
        }
        public DateTime SelectedPreviousDate
        {
            get { return _selectedPreviousDate; }
            set
            {
                _selectedPreviousDate = value;
                OnPropertyChanged(nameof(SelectedPreviousDate));
            }
        }
        public Tag TagToRemove
        {
            get { return _tagToRemove; }
            set
            {
                _tagToRemove = value;
                OnPropertyChanged(nameof(TagToRemove));
            }
        }
        public DateTime PreviousDateToRemove
        {
            get { return _previousDateToRemove; }
            set
            {
                _previousDateToRemove = value;
                OnPropertyChanged(nameof(PreviousDateToRemove));
            }
        }
        public List<ComboBoxData<Attendance>> AttendanceDropdownOptions { get; set; }
        public List<ComboBoxData<EventType>> EventTypeDropdownOptions { get; set; }
        public List<ComboBoxData<Tag>> TagDropdownOptions { get; set; }
        #endregion properties
        #region commands
        private RelayCommand _addTagCommand;
        private RelayCommand _addPreviousDateCommand;
        private RelayCommand _removeTagCommand;
        private RelayCommand _removePreviousDateCommand;
        public RelayCommand AddTagCommand
        {
            get
            {
                return _addTagCommand ??= new RelayCommand(param => AddTagCommandExecute(), param => CanAddTagCommandExecute());
            }
        }
        public RelayCommand AddPreviousDateCommand
        {
            get
            {
                return _addPreviousDateCommand ??= new RelayCommand(param => AddPreviousDateCommandExecute(), param => CanAddPreviousDateCommandExecute());
            }
        }
        public RelayCommand RemoveTagCommand
        {
            get
            {
                return _removeTagCommand ??= new RelayCommand(param => RemoveTagCommandExecute(), param => CanRemoveTagCommandExecute());
            }
        }
        public RelayCommand RemovePreviousDateCommand
        {
            get
            {
                return _removePreviousDateCommand ??= new RelayCommand(param => RemovePreviousDateCommandExecute(), param => CanRemovePreviousDateCommandExecute());
            }
        }
        public void RemoveTagCommandExecute()
        {
            Tags.Remove(TagToRemove);
        }
        public bool CanRemoveTagCommandExecute()
        {
            return TagToRemove != null;
        }
        public void RemovePreviousDateCommandExecute()
        {
            PreviousEventDates.Remove(PreviousDateToRemove);
        }
        public bool CanRemovePreviousDateCommandExecute()
        {
            return PreviousDateToRemove != default;
        }
        public void AddTagCommandExecute()
        {
            if (tagAlreadyAdded(SelectedTag.Value))
            {
                return;
            }
            else
            {
                Tags.Add(SelectedTag.Value);
            }
        }
        public bool CanAddTagCommandExecute()
        {
            return SelectedTag != null;
        }
        public void AddPreviousDateCommandExecute()
        {
            if (dateAlreadyAdded(SelectedPreviousDate))
            {
                return;
            }
            else
            {
                PreviousEventDates.Add(SelectedPreviousDate);
            }
        }
        public bool CanAddPreviousDateCommandExecute()
        {
            return SelectedPreviousDate != default && ValidationUtil.AreDatesInPast([SelectedPreviousDate]);
        }
        public override bool CanOkCommandExecute()
        {
            return ValidationUtil.ValidateTextInputIsOnlyLetters([Label, Name, Description, Country, City]) &&
                ValidationUtil.StringsHaveValue([Label, Name, Description, Country, City]) &&
                ValidationUtil.InputHasValue(EventDate) &&
                ValidationUtil.IsDateInFuture(EventDate) &&
                ValidationUtil.InputHasValue(EventType) &&
                ValidationUtil.InputHasValue(Attendance) &&
                ValidationUtil.DecimalValueValidation([AverageHostingExpenses]);
        }
        public override void OkCommandExecute()
        {
            Event editedEvent = new()
            {
                Id = Id,
                Label = Label,
                Name = Name,
                Description = Description,
                Type = EventType.Value,
                Attendance = Attendance.Value,
                IconUrl = IconUrl,
                IsCharity = IsCharity,
                AverageHostingExpenses = decimal.Parse(AverageHostingExpenses),
                Country = Country,
                City = City,
                PreviousEventDates = new List<DateTime>(PreviousEventDates),
                EventDate = EventDate,
                Tags = new List<Tag>(Tags)
            };

            Repository.Instance.EditEvent(editedEvent);
            base.OkCommandExecute();
        }
        #endregion commands
        #region constructors
        public EditEventViewModel(Event selectedEvent)
        {
            AttendanceDropdownOptions = Repository.Instance.attendanceDropdownOptions();
            EventTypeDropdownOptions = Repository.Instance.eventTypeDropdownOptions();
            TagDropdownOptions = Repository.Instance.tagDropdownOptions();
            setProperties(selectedEvent);
            EntityName = "Event";
        }
        private void setProperties(Event selectedEvent)
        {
            Id = selectedEvent.Id;
            Label = selectedEvent.Label;
            Name = selectedEvent.Name;
            Description = selectedEvent.Description;
            EventType = EventTypeDropdownOptions.Find(option => option.Value == selectedEvent.Type);
            Attendance = AttendanceDropdownOptions.Find(option => option.Value == selectedEvent.Attendance);
            IconUrl = selectedEvent.IconUrl;
            IsCharity = selectedEvent.IsCharity;
            AverageHostingExpenses = (selectedEvent.AverageHostingExpenses / 100).ToString("F2", CultureInfo.InvariantCulture);
            Country = selectedEvent.Country;
            City = selectedEvent.City;
            EventDate = selectedEvent.EventDate;
            Tags = new ObservableCollection<Tag>(selectedEvent.Tags);
            PreviousEventDates = new ObservableCollection<DateTime>(selectedEvent.PreviousEventDates);

        }
        #endregion constructors
        #region functions
        private bool tagAlreadyAdded(Tag checkedTag)
        {
            Tag? existingTag = Tags.FirstOrDefault(foundTag => foundTag.Id == checkedTag.Id);
            if (existingTag != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool dateAlreadyAdded(DateTime date)
        {
            DateTime existingDate = PreviousEventDates.FirstOrDefault(foundDate => foundDate == date);
            if (existingDate != default)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion functions
    }
}
