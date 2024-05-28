using EventLocator.Data;
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
        Point StartPoint = new();
        private TextBlock? DraggedTextBlock;
        private int counter = 0;
        public MapView()
        {
            DataContext = new MapViewModel();
            InitializeComponent();
            LoadMapEvents();
        }
        private void EventList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }
        private void EventList_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

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

                if(draggedEvent is Event)
                {
                    if (draggedEvent.Position_X == null && draggedEvent.Position_Y == null)
                    {
                        TextBlock droppedTextBlock = CreateMapEventElement(draggedEvent);
                        Map.Children.Add(droppedTextBlock);
                        SetMapEventLocation(draggedEvent, droppedTextBlock, dropPosition);
                        (DataContext as MapViewModel).MoveEventFromListToMap(draggedEvent);
                        counter++;
                    }
                    else
                    {
                        if (DraggedTextBlock != null)
                        {
                            SetMapEventLocation(draggedEvent, DraggedTextBlock, dropPosition);
                            (DataContext as MapViewModel).MoveEventOnMap(draggedEvent);
                        }
                    }
                }
            }
        }
        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                textBlock.CaptureMouse();
                DraggedTextBlock = textBlock;
                DataObject dragData = new("event", textBlock.DataContext);
                DragDrop.DoDragDrop(textBlock, dragData, DragDropEffects.Move);
            }
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("event"))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.Move;
            }
        }
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("event"))
            {
                Event draggedEvent = e.Data.GetData("event") as Event;

                // Add event back to the ListView
                (DataContext as MapViewModel).MoveEventFromMapToList(draggedEvent);
                var numb = Map.Children;
                // Remove from canvas
                if (DraggedTextBlock != null)
                {
                    Map.Children.Remove(DraggedTextBlock);
                    DraggedTextBlock = null;
                }
            }
        }
        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.IsMouseCaptured)
            {
                Point currentPoint = e.GetPosition(Map);

                double offsetX = currentPoint.X - StartPoint.X;
                double offsetY = currentPoint.Y - StartPoint.Y;

                double newLeft = Canvas.GetLeft(textBlock) + offsetX;
                double newTop = Canvas.GetTop(textBlock) + offsetY;

                Canvas.SetLeft(textBlock, newLeft);
                Canvas.SetTop(textBlock, newTop);

                (textBlock.DataContext as Event).Position_X = newLeft;
                (textBlock.DataContext as Event).Position_Y = newTop;

                StartPoint = currentPoint;
            }
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                Repository.Instance.EditEvent(textBlock.DataContext as Event);
                textBlock.ReleaseMouseCapture();
            }
        }
        #region functions
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void LoadMapEvents()
        {
            foreach (Event e in (DataContext as MapViewModel).MapEvents)
            {
                TextBlock droppedTextBlock = CreateMapEventElement(e);
                Canvas.SetLeft(droppedTextBlock, (double)e.Position_X);
                Canvas.SetTop(droppedTextBlock, (double)e.Position_Y);

                Map.Children.Add(droppedTextBlock);
            };
        }
        private TextBlock CreateMapEventElement(Event draggedEvent)
        {
            TextBlock textBlock = new()
            {
                Text = draggedEvent.Name,
                DataContext = draggedEvent,
                Background = new SolidColorBrush(Colors.LightBlue),
                Padding = new Thickness(5),
                Cursor = Cursors.Hand
            };
            textBlock.PreviewMouseLeftButtonDown += TextBlock_PreviewMouseLeftButtonDown;
            textBlock.MouseMove += TextBlock_MouseMove;
            textBlock.PreviewMouseLeftButtonUp += TextBlock_PreviewMouseLeftButtonUp;
            return textBlock;
        }
        private static void SetMapEventLocation(Event draggedEvent, TextBlock element, Point newPosition)
        {
            Canvas.SetLeft(element, newPosition.X);
            Canvas.SetTop(element, newPosition.Y);

            draggedEvent.Position_X = newPosition.X;
            draggedEvent.Position_Y = newPosition.Y;
        }
        #endregion functions
    }
}
