using EventLocator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Models
{
    public class Entity : ViewModelBase
    {
        protected Guid id;
        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }
}
