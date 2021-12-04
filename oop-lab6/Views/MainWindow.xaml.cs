﻿using oop_lab6.ViewModels;
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

namespace oop_lab6
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
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shapesComboBox.ItemsSource = 
                Enum.GetValues(typeof(MainWindowViewModel.Shapes)).Cast<MainWindowViewModel.Shapes>();
            shapesComboBox.SelectedIndex = 0;
        }

        private void canvasImage_Loaded(object sender, RoutedEventArgs e)
        {
            DrawShapes();
        }
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
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
                    ViewModel.DeselectAll();
                    ViewModel.AddShape(
                        (int)e.GetPosition(canvas).X,
                        (int)e.GetPosition(canvas).Y,
                        (MainWindowViewModel.Shapes)shapesComboBox.SelectedIndex);
                    DrawShapes();
                }
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
                {
                    ViewModel.DeselectAll();
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
        private void canvasImage_MouseMove(object sender, MouseEventArgs e)
        {
            if(dragging && e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.Point currentMousePos = e.GetPosition(canvas);
                ViewModel.ShiftSelectedShapes(
                    (int)(currentMousePos.X - draggingFrom.X), 
                    (int)(currentMousePos.Y - draggingFrom.Y));
                draggingFrom = currentMousePos;
                DrawShapes();
            }
        }
        private void window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ViewModel.DeleteSelected();
                DrawShapes();
            }
        }

        void DrawShapes()
        {
            using (var bmp = new Bitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(System.Drawing.Color.Transparent);
                for(ViewModel.ShapeContainer.First(); 
                    ViewModel.ShapeContainer.IsEOL() == false; 
                    ViewModel.ShapeContainer.Next())
                {
                    ViewModel.ShapeContainer.GetCurrent().DrawItself(gfx);
                }
                canvasImage.Source = BmpImageFromBmp(bmp);
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
    }
}