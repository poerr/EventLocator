using EventLocator.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventLocator.Domain.EventTypes.Details
{
    /// <summary>
    /// Interaction logic for DetailsEventTypeView.xaml
    /// </summary>
    public partial class DetailsEventTypeView : Page
    {
        public DetailsEventTypeView(EventType eventType)
        {
            DataContext = new DetailsEventTypeViewModel(eventType);
            InitializeComponent();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteSection.Visibility = Visibility.Visible;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DeleteSection.Visibility = Visibility.Collapsed;
        }
    }
}
