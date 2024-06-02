using Accessibility;
using EventLocator.Data;
using EventLocator.Domain.Events.Edit;
using EventLocator.Domain.EventTypes.Details;
using EventLocator.Domain.EventTypes.Edit;
using EventLocator.Domain.Models;
using EventLocator.Domain.Tags.Details;
using EventLocator.Domain.Tags.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        private RelayCommand _detailsCommand;
        private RelayCommand _listCommand;
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
        public RelayCommand DetailsCommand
        {
            get
            {
                return _detailsCommand ??= new RelayCommand(param => DetailsCommandExecute(), param => CanDetailsCommandExecute());
            }
        }
        public RelayCommand ListCommand
        {
            get
            {
                return _listCommand ??= new RelayCommand(param =>  ListCommandExecute(), param => CanListCommandExecute());
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
            DeleteAfterOk(SelectedEntity);
            SelectedEntity = default;
        }
        public virtual void DeleteAfterOk(Entity item) { }
        public virtual bool CanSearchCommandExecute()
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
        public bool CanDetailsCommandExecute()
        {
            return SelectedEntity != null;
        }
        public virtual void DetailsCommandExecute() { }
        public bool CanListCommandExecute()
        {
            return true;
        }
        public virtual void ListCommandExecute() { }
        #endregion commands
        #region functions
        public virtual void LoadTableData() { }
        public virtual void FilterEntities() { }
        public virtual void ClearSearchFields() { }
        protected void RefreshDataOnDialog_Closed(object? sender, EventArgs e)
        {
            LoadTableData();
        }
        protected void NavigateToPage(string pageName, string entityName, Entity selected = default)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var frame = mainWindow.MainFrame;

            if (selected != null)
            {
                NavigateToPageWithEntity(selected, frame, pageName);
            }
            else
            {
                string uriPath = "Domain/" + entityName + "s" + "/" + pageName + "/" + pageName + entityName + "View.xaml";
                frame.Navigate(new Uri(uriPath, UriKind.RelativeOrAbsolute));
            }
        }

        protected void NavigateToPageWithEntity(Entity selected, Frame frame, string action)
        {
            Type entityType = selected.GetType();

            if(entityType == typeof(Event))
            {
                if(action == "Edit")
                {
                    frame.Navigate(new EditEventView(selected as Event));
                }
                else if(action == "Details")
                {
                    
                }
            }
            else if(entityType == typeof(EventType))
            {
                if (action == "Edit")
                {
                    frame.Navigate(new EditEventTypeView(selected as EventType));
                }
                else if (action == "Details")
                {
                    frame.Navigate(new DetailsEventTypeView(selected as EventType));
                }
            }
            else
            {
                if (action == "Edit")
                {
                    frame.Navigate(new EditTagView(selected as Tag));
                }
                else if (action == "Details")
                {
                    frame.Navigate(new DetailsTagView(selected as Tag));
                }
            }
        }
        #endregion functions
    }
}