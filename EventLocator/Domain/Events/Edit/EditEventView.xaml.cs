using EventLocator.Domain.Events.Add;
using EventLocator.Domain.Models;
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

namespace EventLocator.Domain.Events.Edit
{
    /// <summary>
    /// Interaction logic for EditEventView.xaml
    /// </summary>
    public partial class EditEventView : Window
    {
        public EditEventView(Event selectedEvent)
        {
            DataContext = new EditEventViewModel(selectedEvent);
            InitializeComponent();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //(DataContext as EditEventViewModel).EditAfterOk();
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
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
