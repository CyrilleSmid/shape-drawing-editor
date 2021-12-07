using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    public class CCircle : CBasicShape
    {
        public CCircle(int posX, int posY)
            : base(posX, posY) { }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            using (var brush = new SolidBrush(FillColor))
            {
                gfx.FillEllipse(brush,
               x - size,
               y - size,
               size * 2,
               size * 2);
                gfx.DrawEllipse(Selected ? selectedPen : defaultPen,
               x - size,
               y - size,
               size * 2,
               size * 2);
            }
               
        }

        public override bool IfInside(int posX, int posY) 
{
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= size;
        }
    }
}
