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

namespace EventLocator.Domain.Tags.Details
{
    /// <summary>
    /// Interaction logic for DetailsTagView.xaml
    /// </summary>
    public partial class DetailsTagView : Page
    {
        public DetailsTagView(Tag tag)
        {
            DataContext = new DetailsTagViewModel(tag);
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
