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
using System.Windows.Shapes;

namespace EventLocator.Domain.Tags.Edit
{
    /// <summary>
    /// Interaction logic for EditTagView.xaml
    /// </summary>
    public partial class EditTagView : Window
    {
        public EditTagView(Tag tag)
        {
            DataContext = new EditTagViewModel(tag);
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditTagViewModel).EditAfterOk();
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
