using EventLocator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventLocator.Domain.Tags.Add
{
    /// <summary>
    /// Interaction logic for AddTagView.xaml
    /// </summary>
    public partial class AddTagView : Page
    {
        public AddTagView()
        {
            DataContext = new AddTagViewModel();    
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AddTagViewModel).AddAfterOk();
            //Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //Close();
        }
    }
}
