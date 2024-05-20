using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Common
{
    public class BaseDialogViewModel : ViewModelBase
    {
        private RelayCommand _okCommand;
        private RelayCommand _cancelCommand;

        private DialogState _diaologState;

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
        public DialogState DialogState
        {
            get
            {
                return _diaologState;
            }
            set
            {
                _diaologState = value;
                OnPropertyChanged(nameof(DialogState));
            }
        }


        public virtual void OkCommandExecute()
        {
            if(DialogState == DialogState.ADD)
            {
                AddAfterOk();
            }
            else if(DialogState == DialogState.EDIT)
            {
                EditAfterOk();
            }
            DialogState = DialogState.VIEW;
        }

        public virtual bool CanOkCommandExecute()
        {
            // napravi validaciju forme
            return true;
        }
        public virtual void CancelCommandExecute()
        {
            DialogState = DialogState.VIEW;

        }
        public virtual bool CanCancelCommandExecute()
        {
            return true;
        }
        public virtual void AddAfterOk() { }
        public virtual void EditAfterOk() { }
    }
}
