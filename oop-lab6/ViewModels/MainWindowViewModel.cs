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

        Container<CCircle> _shapeContainer = new Container<CCircle>();
        public Container<CCircle> ShapeContainer { get { return _shapeContainer; } }

        public void AddCircle(int posX, int posY)
        {
            CCircle circle = new CCircle(posX, posY);
            circle.Selected = true;
            ShapeContainer.Append(circle);
        }
        public void SelectCircleAt(int posX, int posY)
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
    }
}
