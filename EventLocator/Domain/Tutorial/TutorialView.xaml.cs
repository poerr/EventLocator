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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventLocator.Domain.Tutorial
{
    /// <summary>
    /// Interaction logic for TutorialView.xaml
    /// </summary>
    public partial class TutorialView : Page
    {
        public TutorialView()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Uri mapUri = new("Resources/Images/MapTutorial.mkv", UriKind.RelativeOrAbsolute);
            mapTutorialPlayer.Source = mapUri;

            Uri searchAndFilterEventsUri = new ("Resources/Images/SearchAndFilterEventsTutorial.mkv", UriKind.RelativeOrAbsolute);
            searchAndFilterEventsPlayer.Source = searchAndFilterEventsUri;

            Uri addEventUri = new ("Resources/Images/AddEventTutorial.mkv", UriKind.RelativeOrAbsolute);
            addEventPlayer.Source = addEventUri;

            Uri editEventUri = new ("Resources/Images/EditEventTutorial.mkv", UriKind.RelativeOrAbsolute);
            editEventPlayer.Source = editEventUri;

            Uri tagManipulationUri = new ("Resources/Images/TagManipulationTutorial.mkv", UriKind.RelativeOrAbsolute);
            tagManipulationPlayer.Source = tagManipulationUri;
        }
        private void PlayMap_Click(object sender, RoutedEventArgs e)
        {
            mapTutorialPlayer.Play();
        }

        private void PauseMap_Click(object sender, RoutedEventArgs e)
        {
            if (mapTutorialPlayer.CanPause)
            {
                mapTutorialPlayer.Pause();
            }
        }

        private void StopMap_Click(object sender, RoutedEventArgs e)
        {
            mapTutorialPlayer.Stop();
        }
        private void PlaySearchAndFilterEvents_Click(object sender, RoutedEventArgs e)
        {
            searchAndFilterEventsPlayer.Play();
        }

        private void PauseSearchAndFilterEvents_Click(object sender, RoutedEventArgs e)
        {
            if (searchAndFilterEventsPlayer.CanPause)
            {
                searchAndFilterEventsPlayer.Pause();
            }
        }

        private void StopSearchAndFilterEvents_Click(object sender, RoutedEventArgs e)
        {
            searchAndFilterEventsPlayer.Stop();
        }

        private void PlayAddEvent_Click(object sender, RoutedEventArgs e)
        {
            addEventPlayer.Play();
        }

        private void PauseAddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (addEventPlayer.CanPause)
            {
                addEventPlayer.Pause();
            }
        }

        private void StopAddEvent_Click(object sender, RoutedEventArgs e)
        {
            addEventPlayer.Stop();
        }

        private void PlayEditEvent_Click(object sender, RoutedEventArgs e)
        {
            editEventPlayer.Play();
        }

        private void PauseEditEvent_Click(object sender, RoutedEventArgs e)
        {
            if (editEventPlayer.CanPause)
            {
                editEventPlayer.Pause();
            }
        }

        private void StopEditEvent_Click(object sender, RoutedEventArgs e)
        {
            editEventPlayer.Stop();
        }

        private void PlayTagManipulation_Click(object sender, RoutedEventArgs e)
        {
            tagManipulationPlayer.Play();
        }

        private void PauseTagManipulation_Click(object sender, RoutedEventArgs e)
        {
            if (tagManipulationPlayer.CanPause)
            {
                tagManipulationPlayer.Pause();
            }
        }

        private void StopTagManipulation_Click(object sender, RoutedEventArgs e)
        {
            tagManipulationPlayer.Stop();
        }
    }
}
