using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6.Models
{
    internal class CSquare : CShape
    {
        private const int size = 16;
        public CSquare(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            {
                gfx.DrawRectangle(Selected ? selectedPen : defaultPen,
                x - size / 2,
                y - size / 2,
                size,
                size);
            }

        }

        public override bool IfInside(int posX, int posY)
        {
            return Math.Abs(x - posX) <= size / 2 && Math.Abs(y - posY) <= size / 2;
        }


    }
}
