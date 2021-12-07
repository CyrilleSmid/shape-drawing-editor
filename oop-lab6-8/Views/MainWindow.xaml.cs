using oop_lab6_8.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
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

namespace oop_lab6_8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindowView
    {
        public MainWindowViewModel ViewModel { get; set; }
        public MainWindow()
        {
            ViewModel = new MainWindowViewModel(this);
            DataContext = ViewModel;

            InitializeComponent();
            stopwatch.Start();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shapesComboBox.ItemsSource = 
                Enum.GetValues(typeof(Models.Shapes)).Cast<Models.Shapes>();
            shapesComboBox.SelectedIndex = 0;
        }

        private void canvasImage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CanvasSizeChanged();
            DrawShapes();
            ViewModel.MaxSize = (int)(Math.Min(canvas.ActualWidth, canvas.ActualHeight) / 2 - 6);
        }

        public System.Windows.Point GetCurentCanvasSize()
        {
            return new System.Windows.Point(
                (int)canvas.ActualWidth,
                (int)canvas.ActualHeight);
        }
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewModel.CanvasSizeChanged();
            DrawShapes();
        }

        private void canvasImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    dragging = true;
                    draggingFrom = e.GetPosition(canvas);
                }
                else
                {
                    ViewModel.DeselectAllShapes();
                    ViewModel.AddShape(
                        (int)e.GetPosition(canvas).X,
                        (int)e.GetPosition(canvas).Y,
                        (Models.Shapes)shapesComboBox.SelectedIndex);
                    DrawShapes();
                }
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
                {
                    ViewModel.DeselectAllShapes();
                }
                ViewModel.SelectShapeAt(
                    (int)e.GetPosition(canvas).X,
                    (int)e.GetPosition(canvas).Y);
                DrawShapes();
            }
        }
        private bool dragging = false;
        private System.Windows.Point draggingFrom = new System.Windows.Point();
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
        }
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        Stopwatch stopwatch = new Stopwatch();
        int shiftUpdateFrequency = 40;
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            if(stopwatch.ElapsedMilliseconds > shiftUpdateFrequency &&
               dragging && e.LeftButton == MouseButtonState.Pressed && 
               Keyboard.IsKeyDown(Key.LeftShift))
            {
                System.Windows.Point currentMousePos = e.GetPosition(canvas);
                int shiftX = (int)(currentMousePos.X - draggingFrom.X);
                int shiftY = (int)(currentMousePos.Y - draggingFrom.Y);
                ViewModel.ShiftSelectedShapes(shiftX, shiftY);
                draggingFrom = currentMousePos;
                //Debug.WriteLine($"Dragging({shiftX}, {shiftY})");
                DrawShapes();

                stopwatch.Restart();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                ViewModel.ShiftSelectedShapes(-10, 0);
            }
            else if (e.Key == Key.Up)
            {
                ViewModel.ShiftSelectedShapes(0, -10);
            }
            else if(e.Key == Key.Right)
            {
                ViewModel.ShiftSelectedShapes(10, 0);
            }
            else if(e.Key == Key.Down)
            {
                ViewModel.ShiftSelectedShapes(0, 10);
            }
            else if(e.Key == Key.OemPlus)
            {
                ViewModel.SelectedShapeSize += 20;
                ViewModel.ResizeSelectedShapes();
            }
            else if(e.Key == Key.OemMinus)
            {
                ViewModel.SelectedShapeSize -= 20;
                ViewModel.ResizeSelectedShapes();
            }
            else if(e.Key == Key.Delete)
            {
                ViewModel.DeleteSelectedShapes();
            }
            else if(e.Key == Key.Z && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                ViewModel.UndoLastCommand();
            }
            DrawShapes();
        }
        private void resizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ViewModel.ResizeSelectedShapes();
            DrawShapes();
        }

        private void colorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ViewModel.ChangeSelectedShapesColor();
            DrawShapes();
        }

        void DrawShapes()
        {
            if(canvas.IsLoaded)
            {
                using (var bmp = new Bitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight))
                using (var gfx = Graphics.FromImage(bmp))
                {
                    gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gfx.Clear(System.Drawing.Color.Transparent);
                    for (ViewModel.ShapeContainer.First();
                        ViewModel.ShapeContainer.IsEOL() == false;
                        ViewModel.ShapeContainer.Next())
                    {
                        ViewModel.ShapeContainer.GetCurrent().DrawItself(gfx);
                    }
                    canvasImage.Source = BmpImageFromBmp(bmp);
                }
            }
        }
        private BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        System.Drawing.Color getRecourceAsColor(string name)
        {
            SolidColorBrush resourceColor = FindResource(name) as SolidColorBrush;
            return System.Drawing.Color.FromArgb(
                resourceColor.Color.A,
                resourceColor.Color.R,
                resourceColor.Color.G,
                resourceColor.Color.B);
        }

        private void GroupSelection_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GroupSelectedShapes();
        }
        private void UngroupSelection_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UngroupSelection();
        }
    }
}