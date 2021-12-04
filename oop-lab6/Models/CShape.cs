using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace oop_lab6.Models
{
    public abstract class CShape
    {
        protected int x, y;
        protected const int thickness = 5;

        protected Color color;
        protected Color highlightColor;
        public abstract void DrawItself(Graphics gfx);

        public abstract bool IfInside(int posX, int posY);

        public abstract void SwitchSelection();
    }
}
