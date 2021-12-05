using oop_lab6.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6.Models;
using System.Diagnostics;


namespace oop_lab6.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public IMainWindowView View { get; set; }
        public MainWindowViewModel(IMainWindowView view)
        {
            View = view;
        }

        Container<CShape> _shapeContainer = new Container<CShape>();
        public Container<CShape> ShapeContainer { get { return _shapeContainer; } }

        public enum Shapes
        { 
            Circle,
            Square,
            Triangle
        }

        public void AddShape(int posX, int posY, Shapes shapeType)
        {
            CShape shape;
            switch(shapeType)
            {
                case Shapes.Square:
                    shape = new CSquare(posX, posY,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                    break;
                case Shapes.Triangle:
                    shape = new CEquilateralTriangle(posX, posY,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                    break;
                default:
                    shape = new CCircle(posX, posY,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                    break;
            }
            
            shape.Selected = true;
            SelectedShapeSize = shape.Size;
            SelectedShapeColor = System.Windows.Media.Color.FromArgb(
                shape.FillColor.A,
                shape.FillColor.R,
                shape.FillColor.G,
                shape.FillColor.B);

            ShapeContainer.Append(shape);
        }
        public void SelectShapeAt(int posX, int posY)
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().IfInside(posX, posY))
                {
                    ShapeContainer.GetCurrent().SwitchSelection();
                }
            }
            UpdateSelectedShape();
        }
        private void UpdateSelectedShape()
        {
            bool anySelected = false;
            int newSelectedSize = 0;
            System.Drawing.Color newSelectedColor = new System.Drawing.Color();
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().Selected)
                {
                    if(anySelected)
                    {
                        return;
                    }
                    anySelected = true;
                    newSelectedSize = ShapeContainer.GetCurrent().Size;
                    newSelectedColor = ShapeContainer.GetCurrent().FillColor;
                }
            }
            SelectedShapeSize = newSelectedSize;
            SelectedShapeColor = System.Windows.Media.Color.FromArgb(
                newSelectedColor.A,
                newSelectedColor.R,
                newSelectedColor.G,
                newSelectedColor.B);
        }
        public void DeselectAllShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                ShapeContainer.GetCurrent().Selected = false;
            }
        }
        public void DeleteSelectedShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().Selected)
                {
                    ShapeContainer.DeleteCurrent();
                }
            }
        }
        public void ShiftSelectedShapes(int shiftX, int shiftY)
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    ShapeContainer.GetCurrent().ShiftPos(
                        shiftX, shiftY,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                }
            }
        }

        private const int minSize = 10;
        private int maxSize;
        public int MinSize { get { return minSize; } }
        public int ShapeThickness { get { return CShape.Thickness; } }
        public int MaxSize 
        { 
            get { return maxSize; } 
            set 
            { 
                maxSize = value;
                OnPropertyChanged();
            } 
        }

        private int _selectedSize = 20;
        public int SelectedShapeSize
        {
            get { return _selectedSize; }
            set 
            {
                value = Math.Min(Math.Max(minSize, value), maxSize);
                if(_selectedSize != value)
                {
                    //Debug.WriteLine($"Size = {value}");
                    _selectedSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ResizeSelectedShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    ShapeContainer.GetCurrent().Resize(
                        SelectedShapeSize,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                }
            }
        }
        public void CanvasSizeChanged()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                ShapeContainer.GetCurrent().FitNewCanvasSize(
                    (int)View.GetCurentCanvasSize().X,
                    (int)View.GetCurentCanvasSize().Y);
            }
        }

        private System.Windows.Media.Color _selectedShapeColor = new System.Windows.Media.Color();
        private System.Windows.Media.Color SelectedShapeColor 
        {
            get { return _selectedShapeColor; }
            set 
            { 
                _selectedShapeColor = value;
                OnPropertyChanged(nameof(ShapeColorAlpha));
                OnPropertyChanged(nameof(ShapeColorRed));
                OnPropertyChanged(nameof(ShapeColorGreen));
                OnPropertyChanged(nameof(ShapeColorBlue));
            }
        }
        public byte ShapeColorAlpha
        {
            get { return SelectedShapeColor.A; }
            set
            {
                _selectedShapeColor.A = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorRed
        {
            get { return SelectedShapeColor.R; }
            set
            {
                _selectedShapeColor.R = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorGreen
        {
            get { return SelectedShapeColor.G; }
            set
            {
                _selectedShapeColor.G = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorBlue
        {
            get { return SelectedShapeColor.B; }
            set
            {
                _selectedShapeColor.B = value;
                OnPropertyChanged();
            }
        }

        public void ChangeSelectedShapesColor()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    ShapeContainer.GetCurrent().ChangeColor(System.Drawing.Color.FromArgb(
                        SelectedShapeColor.A,
                        SelectedShapeColor.R,
                        SelectedShapeColor.G,
                        SelectedShapeColor.B));
                }
            }
        }
    }
}
