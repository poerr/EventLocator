using EventLocator.Data;
using EventLocator.Domain.Events.Index;
using EventLocator.Domain.EventTypes.Index;
using EventLocator.Domain.Tags.Index;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventLocator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.WorkArea.Width;
            double screenHeight = SystemParameters.WorkArea.Height;

            Width = screenWidth;
            Height = screenHeight;

            Top = 0;
            Left = 0;

            WindowState = WindowState.Maximized;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string pageName = menuItem.Header.ToString();

            switch(pageName)
            {
                case "Map":
                    MainFrame.Content = new IndexEventsView();
                    break;
                case "Events":
                    MainFrame.Content = new IndexEventsView();
                    break;
                case "Event types":
                    MainFrame.Content = new IndexEventTypesView();
                    break;
                case "Tags":
                    MainFrame.Content = new IndexTagsView();
                    break;
                case "Help":
                    break;
            }
        }
    }
}