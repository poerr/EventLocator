using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EventLocator.Common
{
    public class BaseFormPageViewModel : ViewModelBase
    {
        #region properties
        private string _entityName;
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(param => OkCommandExecute(), param => CanOkCommandExecute());
            }
        }
        public RelayCommand CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute());
            }
        }
        public string EntityName
        {
            get { return _entityName; }
            set
            {
                _entityName = value;
                OnPropertyChanged(nameof(EntityName));
            }
        }
        #endregion properties
        #region commands
        public virtual bool CanOkCommandExecute()
        {
            return true;
        }
        public virtual void OkCommandExecute()
        {
            NavigateToIndex(EntityName);
        }
        public virtual bool CanCancelCommandExecute()
        {
            return true;
        }
        public virtual void CancelCommandExecute()
        {
            NavigateToIndex(EntityName);
        }
        public virtual void NavigateToIndex(string entityName)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var frame = mainWindow.MainFrame;

            string uriPath = "Domain/" + entityName + "s" + "/Index/" + "Index" + entityName + "View.xaml";
            frame.Navigate(new Uri(uriPath, UriKind.RelativeOrAbsolute));
        }
        #endregion commands
    }
}
