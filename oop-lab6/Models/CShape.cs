using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace oop_lab6.Models
{
    public abstract class CShape
    {
        protected int x, y;
        protected int size = 16;
        protected const int thickness = 5;

        protected Color color = Color.FromArgb(55, 53, 47);
        protected Color highlightColor = Color.FromArgb(107, 181, 255);

        public bool Selected { get; set; }

        public abstract void DrawItself(Graphics gfx);

        public abstract bool IfInside(int posX, int posY);

        public void ShiftPos(int shiftX, int shiftY)
        {
            x = x + shiftX;
            y = y + shiftY;
        }

        public void SwitchSelection()
        {
            Selected = Selected ? false : true;
        }
    }
}
