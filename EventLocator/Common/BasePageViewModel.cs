using Accessibility;
using EventLocator.Data;
using EventLocator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EventLocator.Common
{
    public class BasePageViewModel<Entity> : ViewModelBase
    {
        #region properties
        private Entity? _selectedEntity;
        private ObservableCollection<Entity> _entities = new();
        private ObservableCollection<Entity> _searchedEntities = new();
        private string _filter;

        public Entity? SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }
        public ObservableCollection <Entity> Entities
        {
            get { return _entities; }
            set
            {
                _entities = value;
                OnPropertyChanged(nameof(Entities));
            }
        }
        public ObservableCollection<Entity> SearchedEntities
        {
            get { return _searchedEntities; }
            set
            {
                _searchedEntities = value;
                OnPropertyChanged(nameof(SearchedEntities));
            }
        }
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged(nameof(Filter));
                    FilterEntities();
                }
            }
        }
        #endregion properties

        #region commands
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _searchCommand;
        private RelayCommand _clearSearchCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand(param => AddCommandExecute(), param => CanAddCommandExecute());
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??= new RelayCommand(param => EditCommandExecute(), param => CanEditCommandExecute());
            }
        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand(param => DeleteCommandExecute(), param => CanDeleteCommandExecute());
            }
        }
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ??= new RelayCommand(param => SearchCommandExecute(), param => CanSearchCommandExecute());
            }
        }
        public RelayCommand ClearSearchCommand
        {
            get
            {
                return _clearSearchCommand ??= new RelayCommand(param => ClearSearchCommandExecute(), param => CanClearSearchCommandExecute());
            }
        }
        public bool CanAddCommandExecute()
        {
            return true;
        }
        public virtual void AddCommandExecute() { }
        public bool CanEditCommandExecute()
        {
            return SelectedEntity != null;
        }
        public virtual void EditCommandExecute() { }
        public bool CanDeleteCommandExecute()
        {
            return SelectedEntity != null;
        }
        public virtual void DeleteCommandExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this item?", "Delete confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                DeleteAfterOk(SelectedEntity);
            }

            SelectedEntity = default;
        }
        public virtual void DeleteAfterOk(Entity item) { }
        public bool CanSearchCommandExecute()
        {
            return true;
        }
        public virtual void SearchCommandExecute() { }
        public bool CanClearSearchCommandExecute()
        {
            return true;
        }
        public virtual void ClearSearchCommandExecute() 
        {
            SearchedEntities = Entities;
            ClearSearchFields(); 
        }
        #endregion commands

        #region functions
        public virtual void LoadTableData() { }
        public virtual void FilterEntities() { }
        public virtual void ClearSearchFields() { }
        protected void RefreshDataOnDialog_Closed(object? sender, EventArgs e)
        {
            LoadTableData();
        }
        #endregion functions
    }
}