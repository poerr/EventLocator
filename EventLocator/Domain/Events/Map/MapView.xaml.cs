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
        #region properties
        Point StartPoint = new();
        private Grid? DraggedTextBlock;
        #endregion properties
        #region constructors
        public MapView()
        {
            DataContext = new MapViewModel();
            InitializeComponent();
            LoadMapEvents();
        }
        #endregion constructors
        #region list events
        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }
        private void List_MouseMove(object sender, MouseEventArgs e)
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
        private void List_DragOver(object sender, DragEventArgs e)
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
        private void List_Drop(object sender, DragEventArgs e)
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
        #endregion list events
        #region map events
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

                if (draggedEvent is Event)
                {
                    if (draggedEvent.Position_X == null && draggedEvent.Position_Y == null)
                    {
                        Grid droppedTextBlock = CreateMapEventElement(draggedEvent);
                        SetMapEventLocation(draggedEvent, droppedTextBlock, dropPosition);
                        Map.Children.Add(droppedTextBlock);
                        (DataContext as MapViewModel).MoveEventFromListToMap(draggedEvent);
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
        #endregion map events
        #region event events
        private void Event_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid textBlock)
            {
                textBlock.CaptureMouse();
                DraggedTextBlock = textBlock;
                DataObject dragData = new("event", textBlock.DataContext);
                DragDrop.DoDragDrop(textBlock, dragData, DragDropEffects.Move);
            }
        }
        private void Event_MouseMove(object sender, MouseEventArgs e)
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
        private void Event_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                Repository.Instance.EditEvent(textBlock.DataContext as Event);
                textBlock.ReleaseMouseCapture();
            }
        }
        #endregion event events
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
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            (DataContext as MapViewModel).FilterMapEvents();
            LoadMapEvents();
        }
        private void LoadMapEvents()
        {
            Map.Children.Clear();
            foreach (Event e in (DataContext as MapViewModel).FilteredMapEvents)
            {
                Grid droppedTextBlock = CreateMapEventElement(e);
                Canvas.SetLeft(droppedTextBlock, (double)e.Position_X);
                Canvas.SetTop(droppedTextBlock, (double)e.Position_Y);

                Map.Children.Add(droppedTextBlock);
            };
        }
        private Grid CreateMapEventElement(Event draggedEvent)
        {
            Grid grid = new Grid
            {
                Background = Brushes.Transparent
            };

            // Add Row Definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            // Create the TextBlock for the label
            TextBlock textBlock = new TextBlock
            {
                Text = "#" + draggedEvent.Label,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold
            };
            Grid.SetRow(textBlock, 0);

            // Create the Image for the icon
            Image image = new Image
            {
                Source = new BitmapImage(new Uri(draggedEvent.IconUrl)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Uniform,
                Width = 30,
                Margin = new Thickness(5)
            };
            Grid.SetRow(image, 1);

            // Add the TextBlock and Image to the Grid
            grid.Children.Add(textBlock);
            grid.Children.Add(image);

            // Create the Rectangle with the desired properties
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.White,
                StrokeThickness = 2,
                RadiusX = 10,
                RadiusY = 10,
                Fill = Brushes.Transparent
            };

            // Create a Border to hold the Rectangle and Grid
            Border border = new Border
            {
                Child = grid,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10),
                Background = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255))
            };

            // Create a Grid to layer the Rectangle and the Border
            Grid container = new()
            {
                DataContext = draggedEvent
            };
            container.Children.Add(rectangle);
            container.Children.Add(border);


            container.PreviewMouseLeftButtonDown += Event_PreviewMouseLeftButtonDown;
            container.MouseMove += Event_MouseMove;
            container.PreviewMouseLeftButtonUp += Event_PreviewMouseLeftButtonUp;
            return container;
        }
        private void SetMapEventLocation(Event draggedEvent, Grid element, Point newPosition)
        {
            double newTopLeftX = newPosition.X - element.RenderSize.Width / 2;
            double newTopLeftY = newPosition.Y - element.RenderSize.Height / 2;

            bool isOverlapping = IsEventOverlapping(((Event)element.DataContext).Id, newTopLeftX, newTopLeftY, element.RenderSize.Width, element.RenderSize.Height);

            if(isOverlapping)
            {
                return;
            }
            else
            {
                Canvas.SetLeft(element, newTopLeftX);
                Canvas.SetTop(element, newTopLeftY);

                draggedEvent.Position_X = newPosition.X;
                draggedEvent.Position_Y = newPosition.Y;
            }
        }
        private bool IsEventOverlapping(Guid draggedEventId, double topLeftX, double topLeftY, double width, double height)
        {
            Rect newRect = new(topLeftX, topLeftY, width, height);

            foreach (UIElement element in Map.Children)
            {
                if (element is Grid)
                {
                    if(((Event)(element as Grid).DataContext).Id == draggedEventId)
                    {
                        return false;
                    }
                }

                double existingTopLeftX = Canvas.GetLeft(element);
                double existingTopLeftY = Canvas.GetTop(element);

                Rect existingRect = new(existingTopLeftX, existingTopLeftY, element.RenderSize.Width, element.RenderSize.Height);

                if (newRect.IntersectsWith(existingRect))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion functions
    }
}
