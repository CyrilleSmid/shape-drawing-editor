using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    internal class CShapeGroup : CShape
    {
        Container<CShape> shapeGroup = new Container<CShape>();

        public override Color FillColor
        {
            get 
            {
                shapeGroup.First();
                return shapeGroup.GetCurrent().FillColor;
            }
            set
            {
                for (shapeGroup.First();
                   shapeGroup.IsEOL() == false;
                   shapeGroup.Next())
                {
                    shapeGroup.GetCurrent().FillColor = value;
                }
            }
        }

        private bool _selected = false;
        public override bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                for (shapeGroup.First();
                   shapeGroup.IsEOL() == false;
                   shapeGroup.Next())
                {
                    shapeGroup.GetCurrent().Selected = value;
                }
            }

        }

        public override int Size
        {
            get
            {
                shapeGroup.First();
                return shapeGroup.GetCurrent().Size;
            }
        }

        public override void DrawItself(Graphics gfx)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().DrawItself(gfx);
            }
        }

        public override bool IfInside(int posX, int posY)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                if (shapeGroup.GetCurrent().IfInside(posX, posY))
                {
                    return true;
                }
            }
            return false;
        }

        public override void ShiftPos(
            int shiftX, int shiftY,
            int borderX, int borderY)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().ShiftPos(
                    shiftX, shiftY,
                    borderX, borderY);
            }
        }

        public override void SwitchSelection()
        {
            Selected = Selected ? false : true;
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().Selected = Selected;
            }
        }

        public override void Resize(int size, int borderX, int borderY)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().Resize(size, borderX, borderY);
            }
        }
        public override void ReboundPosition(int borderX, int borderY)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().ReboundPosition(borderX, borderY);
            }
        }
        public void GroupSelectedShapes(Container<CShape> ShapeContainer) 
        {
            // TODO: if only one group is selected
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    shapeGroup.Append(ShapeContainer.GetCurrent());
                    ShapeContainer.DeleteCurrent();
                }
            }
            ShapeContainer.Append(this);
        }
    }
}
