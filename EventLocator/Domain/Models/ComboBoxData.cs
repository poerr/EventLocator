using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLocator.Domain.Models
{
    public class ComboBoxData<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public ComboBoxData(string name, T value) 
        { 
            Name = name;
            Value = value;
        }
    }
}
