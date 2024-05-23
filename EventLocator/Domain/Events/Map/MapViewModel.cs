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
        }
        public void MoveEventFromListToMap(Event draggedEvent)
        {
            Repository.Instance.EditEvent(draggedEvent);

            ListEvents.Remove(draggedEvent);
            MapEvents.Add(draggedEvent);
        }
        #endregion functions
    }
}
