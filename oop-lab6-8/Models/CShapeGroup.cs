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

        public CShapeGroup(int posX, int posY, int borderX, int borderY) : base(posX, posY, borderX, borderY)
        {
        }

        Container<CShape> shapeContainer = new Container<CShape>();

        public override void DrawItself(Graphics gfx)
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().DrawItself(gfx);
            }
        }

        public override bool IfInside(int posX, int posY)
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                if (shapeContainer.GetCurrent().IfInside(posX, posY))
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
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().ShiftPos(
                    shiftX, shiftY,
                    borderX, borderY);
            }
        }

        public override void SwitchSelection()
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().SwitchSelection();
            }
        }

        public override void Resize(int size, int borderX, int borderY)
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().Resize(size, borderX, borderY);
            }
        }
        public override void FitNewCanvasSize(int borderX, int borderY)
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().SetBoundedPos(x, y, borderX, borderY);
            }
        }

        public override void ChangeColor(Color newColor)
        {
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                shapeContainer.GetCurrent().ChangeColor(newColor);
            }
        }
    }
}
