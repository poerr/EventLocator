using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.EventTypes.Add;
using EventLocator.Domain.EventTypes.Edit;
using EventLocator.Domain.Models;
using EventLocator.Domain.Tags.Add;
using EventLocator.Domain.Tags.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EventLocator.Domain.EventTypes.Index
{
    public class IndexEventTypesViewModel : BasePageViewModel<EventType>
    {
        #region properties
        private string _searchedLabel;
        private string _searchedName;
        private string _searchedDescription;

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
        #endregion properties

        #region constructors
        public IndexEventTypesViewModel()
        {
            LoadTableData();
        }
        #endregion constructors

        #region commands
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            AddEventTypeView addEventTypeView = new();
            addEventTypeView.Closed += RefreshDataOnDialog_Closed;
            addEventTypeView.ShowDialog();
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            EditEventTypeView editTagView = new(SelectedEntity);
            editTagView.Closed += RefreshDataOnDialog_Closed;
            editTagView.ShowDialog();
        }
        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
        }
        public override void DeleteAfterOk(EventType item)
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteEventType(item.Id);
            LoadTableData();
        }
        public override void SearchCommandExecute()
        {
            base.SearchCommandExecute();

            if (!string.IsNullOrEmpty(SearchedLabel))
            {
                SearchedEntities = new ObservableCollection<EventType>(SearchedEntities.Where(entity => entity.Label.ToLower().Contains(SearchedLabel.ToLower())));
            }

            if (!string.IsNullOrEmpty(SearchedName))
            {
                SearchedEntities = new ObservableCollection<EventType>(SearchedEntities.Where(entity => entity.Name.ToLower().Contains(SearchedName.ToLower())));
            }

            if (!string.IsNullOrEmpty(SearchedDescription))
            {
                SearchedEntities = new ObservableCollection<EventType>(SearchedEntities.Where(entity => entity.Description.ToLower().Contains(SearchedDescription.ToLower())));
            }
        }

        public override void ClearSearchCommandExecute()
        {
            base.ClearSearchCommandExecute();
        }
        #endregion commands

        #region functions
        public override void LoadTableData()
        {
            Entities = new ObservableCollection<EventType>(Repository.Instance.GetAllEventTypes());
            SearchedEntities = Entities;
        }
        public override void FilterEntities()
        {
            base.FilterEntities();

            if (!string.IsNullOrEmpty(Filter))
            {
                string filter = Filter.ToLower();

                SearchedEntities = new ObservableCollection<EventType>(Entities.Where(
                    entity =>
                    entity.Id.ToString().ToLower().Contains(filter) ||
                    entity.Label.ToLower().Contains(filter) ||
                    entity.Name.ToLower().Contains(filter) ||
                    entity.Description.ToLower().Contains(filter)
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
        }
        #endregion functions
    }
}