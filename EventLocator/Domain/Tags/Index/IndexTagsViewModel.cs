using EventLocator.Common;
using EventLocator.Data;
using EventLocator.Domain.Events.Add;
using EventLocator.Domain.Models;
using EventLocator.Domain.Tags.Add;
using EventLocator.Domain.Tags.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Tags.Index
{
    public class IndexTagsViewModel : BasePageViewModel<Tag>
    {
        #region properties
        private string _searchedLabel;
        private string _searchedColor;
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
        public string SearchedColor
        {
            get { return _searchedColor; }
            set
            {
                _searchedColor = value;
                OnPropertyChanged(nameof(SearchedColor));
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
        public IndexTagsViewModel()
        {
            LoadTableData();
            FilterEntities();
        }
        #endregion constructors

        #region commands
        public override void AddCommandExecute()
        {
            base.AddCommandExecute();
            NavigateToPage("Add", "Tag");
        }
        public override void EditCommandExecute()
        {
            base.EditCommandExecute();
            EditTagView editTagView = new(SelectedEntity);
            editTagView.Closed += RefreshDataOnDialog_Closed;
            editTagView.ShowDialog();
        }
        public override void DeleteCommandExecute()
        {
            base.DeleteCommandExecute();
        }
        public override void DeleteAfterOk(Tag item) 
        {
            base.DeleteAfterOk(item);
            Repository.Instance.DeleteTag(item.Id);
            LoadTableData();
        }
        public override void SearchCommandExecute()
        {
            base.SearchCommandExecute();

            if (!string.IsNullOrEmpty(SearchedLabel))
            {
                SearchedEntities = new ObservableCollection<Tag>(SearchedEntities.Where(entity => entity.Label.ToLower().Contains(SearchedLabel.ToLower())));
            }

            if (!string.IsNullOrEmpty(SearchedColor))
            {
                SearchedEntities = new ObservableCollection<Tag>(SearchedEntities.Where(entity => entity.Color.ToLower().Contains(SearchedColor.ToLower())));
            }

            if (!string.IsNullOrEmpty(SearchedDescription))
            {
                SearchedEntities = new ObservableCollection<Tag>(SearchedEntities.Where(entity => entity.Description.ToLower().Contains(SearchedDescription.ToLower())));
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
            Entities = new ObservableCollection<Tag>(Repository.Instance.GetAllTags());
            SearchedEntities = Entities;
        }
        public override void FilterEntities()
        {
            base.FilterEntities();

            if (!string.IsNullOrEmpty(Filter))
            {
                string filter = Filter.ToLower();

                SearchedEntities = new ObservableCollection<Tag>(
                    Entities.Where(
                        entity =>
                        entity.Label.ToLower().Contains(filter) ||
                        entity.Description.ToLower().Contains(filter)
                    )
                );
            }
            else
            {
                SearchedEntities = Entities;
            }
        }
        public override void ClearSearchFields()
        {
            SearchedLabel = string.Empty;
            SearchedColor = string.Empty;
            SearchedDescription = string.Empty;
        }
        #endregion functions
    }
}