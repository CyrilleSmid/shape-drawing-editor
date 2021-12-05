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
        public CSquare(int posX, int posY, int borderX, int borderY)
            : base(posX, posY, borderX, borderY) { }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            {
                gfx.DrawRectangle(Selected ? selectedPen : defaultPen,
                x - size,
                y - size,
                size * 2,
                size * 2);
            }

        }

        public override bool IfInside(int posX, int posY)
        {
            return Math.Abs(x - posX) <= size && Math.Abs(y - posY) <= size;
        }


    }
}
