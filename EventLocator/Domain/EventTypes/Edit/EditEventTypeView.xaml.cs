using EventLocator.Domain.Models;
using EventLocator.Domain.Tags.Edit;
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

namespace EventLocator.Domain.EventTypes.Edit
{
    /// <summary>
    /// Interaction logic for EditEventTypeView.xaml
    /// </summary>
    public partial class EditEventTypeView : Window
    {
        public EditEventTypeView(EventType selectedEventType)
        {
            DataContext = new EditEventTypeViewModel(selectedEventType);
            InitializeComponent();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditEventTypeViewModel).EditAfterOk();
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
