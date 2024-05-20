using EventLocator.Domain.EventTypes.Add;
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

namespace EventLocator.Domain.Events.Add
{
    /// <summary>
    /// Interaction logic for AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Window
    {
        public AddEventView()
        {
            DataContext = new AddEventViewModel();
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AddEventViewModel).AddAfterOk();
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
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
