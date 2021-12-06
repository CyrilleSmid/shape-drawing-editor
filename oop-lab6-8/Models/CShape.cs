using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Diagnostics;

namespace oop_lab6_8.Models
{
    public abstract class CShape
    {
        public CShape() { }
    
        public abstract int Size { get; }

        public abstract Color FillColor { get; set; }

        public abstract bool Selected { get; set; }

        public abstract void DrawItself(Graphics gfx);

        public abstract bool IfInside(int posX, int posY);

        public abstract void ShiftPos(int shiftX, int shiftY, int borderX, int borderY);

        public abstract void SwitchSelection();

        public abstract void Resize(int size, int borderX, int borderY);

        public abstract void ReboundPosition(int borderX, int borderY);
    }
}
