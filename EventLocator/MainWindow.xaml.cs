using EventLocator.Data;
using EventLocator.Domain.Events.Index;
using EventLocator.Domain.Events.Map;
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
        private readonly Repository _repository;

        public MainWindow()
        {
            InitializeComponent();

            _repository = new Repository();

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
            Button menuButton = sender as Button;
            switch(menuButton.Name)
            {
                case "EventButton":
                    MainFrame.Navigate(new Uri("Domain/Events/Index/IndexEventsView.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "EventTypeButton":
                    MainFrame.Navigate(new Uri("Domain/EventTypes/Index/IndexEventTypesView.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "TagButton":
                    MainFrame.Navigate(new Uri("Domain/Tags/Index/IndexTagsView.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "MapButton":
                    MainFrame.Navigate(new Uri("Domain/Events/Map/MapView.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "Tutorial":
                    break;
                case "HelpButton":
                    break;
            }
        }
    }
}