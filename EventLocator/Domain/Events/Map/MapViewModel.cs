using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Events.Map
{
    public class MapViewModel : ViewModelBase
    {
        #region properties
        private ObservableCollection<Event> _listEvents;
        private ObservableCollection<Event> _mapEvents;
        private ObservableCollection<Event> _filteredMapEvents;
        private string _mapEventsFilter;
        public ObservableCollection<Event> ListEvents
        {
            get { return _listEvents; }
            set
            {
                _listEvents = value;
                OnPropertyChanged(nameof(ListEvents));
            }
        }
        public ObservableCollection<Event> MapEvents
        {
            get { return _mapEvents; }
            set
            {
                _mapEvents = value;
                OnPropertyChanged(nameof(MapEvents));
            }
        }
        public ObservableCollection<Event> FilteredMapEvents
        {
            get { return _filteredMapEvents; }
            set
            {
                _filteredMapEvents = value;
                OnPropertyChanged(nameof(FilteredMapEvents));
            }
        }
        public string MapEventsFilter
        {
            get { return _mapEventsFilter; }
            set
            {
                _mapEventsFilter = value;
                OnPropertyChanged(nameof(MapEventsFilter));
            }
        }
        #endregion properties
        #region constructors
        public MapViewModel()
        {
            LoadData();
        }
        #endregion constructors
        #region functions
        public void LoadData()
        {
            ListEvents = new ObservableCollection<Event>(Repository.Instance.GetListEvents());
            MapEvents = new ObservableCollection<Event>(Repository.Instance.GetMapEvents());
            FilteredMapEvents = MapEvents;
        }
        public void MoveEventFromListToMap(Event draggedEvent)
        {
            Repository.Instance.EditEvent(draggedEvent);

            ListEvents.Remove(draggedEvent);
            MapEvents.Add(draggedEvent);
        }
        public void MoveEventFromMapToList(Event draggedEvent)
        {
            if(draggedEvent.Position_X == null && draggedEvent.Position_Y == null)
            {
                return;
            }
            draggedEvent.Position_X = null;
            draggedEvent.Position_Y = null;

            Repository.Instance.EditEvent(draggedEvent);

            ListEvents.Add(draggedEvent);
            MapEvents.Remove(draggedEvent);
        }
        public void MoveEventOnMap(Event draggedEvent)
        {
            Repository.Instance.EditEvent(draggedEvent);
        }
        public void FilterMapEvents()
        {
            if(!string.IsNullOrEmpty(MapEventsFilter))
            {
                string filter = MapEventsFilter.ToLower();

                FilteredMapEvents = new ObservableCollection<Event>(MapEvents.Where(
                    filteredEvent => filteredEvent.Name.ToLower().Contains(filter)
                ));
            }
            else
            {
                FilteredMapEvents = MapEvents;
            }
        }
        #endregion functions
    }
}
