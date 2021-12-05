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
                    shape = new CTriangle(posX, posY,
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
        private const int maxSize = 100;
        public int MinSize { get { return minSize; } }
        public int MaxSize { get { return maxSize; } }

        private int _size = 20;
        public int Size
        {
            get { return _size; }
            set 
            {
                value = Math.Min(Math.Max(minSize, value), maxSize);
                if(_size != value)
                {
                    Debug.WriteLine($"Size = {value}");
                    _size = value;
                    ResizeSelectedShapes();
                    View.UpdatePaintBox();
                    OnPropertyChanged();
                }
            }
        }

        private void ResizeSelectedShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    ShapeContainer.GetCurrent().Resize(
                        Size,
                        (int)View.GetCurentCanvasSize().X,
                        (int)View.GetCurentCanvasSize().Y);
                }
            }
        }
    }
}
