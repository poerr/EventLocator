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

namespace EventLocator.Domain.Events.Map
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : Page
    {
        Point startPoint = new Point();
        public MapView()
        {
            DataContext = new MapViewModel();
            InitializeComponent();
            LoadMapEvents();
        }

        private void LoadMapEvents()
        {
            
            foreach(Event e in (DataContext as MapViewModel).MapEvents)
            {
                TextBlock droppedTextBlock = CreateMapEventElement(e);
                Canvas.SetLeft(droppedTextBlock, (double)e.Position_X);
                Canvas.SetTop(droppedTextBlock, (double)e.Position_Y);

                Map.Children.Add(droppedTextBlock);
            };
        }
        private void EventList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }
        private void EventList_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if(e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || 
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)) 
            {
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null) return;

                Event draggedEvent = (Event)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                DataObject dragData = new("event", draggedEvent);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if(current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while(current != null);
            return null;
        }
        private void Map_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("event") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void Map_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("event"))
            {
                Event draggedEvent = e.Data.GetData("event") as Event;
                
                Point dropPosition = e.GetPosition(Map);

                TextBlock droppedTextBlock = CreateMapEventElement(draggedEvent);
                Canvas.SetLeft(droppedTextBlock, dropPosition.X);
                Canvas.SetTop(droppedTextBlock, dropPosition.Y);

                draggedEvent.Position_X = dropPosition.X;
                draggedEvent.Position_Y = dropPosition.Y;

                Map.Children.Add(droppedTextBlock);

                (DataContext as MapViewModel).MoveEventFromListToMap(draggedEvent);

            }
        }
        private static TextBlock CreateMapEventElement(Event e)
        {
            return new TextBlock()
            {
                Text = e.Name,
                Background = Brushes.LightBlue,
                Padding = new Thickness(5),
                Focusable = true,
            };
        }
    }
}
