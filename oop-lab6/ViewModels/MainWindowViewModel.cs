using oop_lab6.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6.Models;

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
                    shape = new CSquare(posX, posY);
                    break;
                case Shapes.Triangle:
                    shape = new CTriangle(posX, posY);
                    break;
                default:
                    shape = new CCircle(posX, posY);
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
        public void DeselectAll()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                ShapeContainer.GetCurrent().Selected = false;
            }
        }
        public void DeleteSelected()
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
                    ShapeContainer.GetCurrent().ShiftPos(shiftX, shiftY);
                }
            }
        }
    }
}
