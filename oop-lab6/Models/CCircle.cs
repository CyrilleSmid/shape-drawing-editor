using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6.Models;

namespace oop_lab6.Models
{
    public class CCircle : CShape
    {
        private const int radius = 15;
        public bool Selected { get; set; }
            
        public CCircle(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            {
               gfx.DrawEllipse(Selected ? selectedPen : defaultPen,
               x - radius,
               y - radius,
               radius * 2,
               radius * 2);
            }
               
        }

        public override bool IfInside(int posX, int posY) 
{
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= radius;
        }

        public override void SwitchSelection()
{
            Selected = Selected ? false : true;
        }
    }
}
