using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6.Models
{
    public class CCircle
    {
        private int x, y;
        private const int radius = 15;
        public bool Selected { get; set; }
            
        public CCircle(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public void DrawItself(
            Graphics gfx,
            System.Drawing.Pen defaultPen,
            System.Drawing.Pen selectedPen)
        {
            //Debug.WriteLine("CCircle.DrawItself()");
            gfx.DrawEllipse(Selected ? selectedPen : defaultPen,
                x - radius,
                y - radius,
                radius * 2,
                radius * 2);
        }

        public bool IfInside(int posX, int posY)
        {
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= radius;
        }

        public void SwitchSelection()
        {
            Selected = Selected ? false : true;
        }
    }
}
