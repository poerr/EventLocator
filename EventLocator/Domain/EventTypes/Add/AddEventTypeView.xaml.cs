using EventLocator.Domain.Tags.Add;
using Microsoft.Win32;
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

namespace EventLocator.Domain.EventTypes.Add
{
    /// <summary>
    /// Interaction logic for AddEventTypeView.xaml
    /// </summary>
    public partial class AddEventTypeView : Page
    {
        public AddEventTypeView()
        {
            DataContext = new AddEventTypeViewModel();
            InitializeComponent();
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            bool? result = openFileDialog.ShowDialog();

            if(result == true)
            {
                string filePath = openFileDialog.FileName;

                BitmapImage bitmap = new();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath);
                bitmap.EndInit();
                SelectedImage.Source = bitmap;
            }
        }
    }
}
