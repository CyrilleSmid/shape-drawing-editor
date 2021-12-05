using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Diagnostics;

namespace oop_lab6.Models
{
    public abstract class CShape
    {
        public CShape(int posX, int posY, int borderX, int borderY)
        {
            SetBoundedPos(posX, posY, borderX, borderY);
        }

        protected int x, y;
        protected virtual void SetBoundedPos(int posX, int posY, int borderX, int borderY)
        {
            x = Math.Min(Math.Max(size + thickness, posX), borderX - size - thickness);
            y = Math.Min(Math.Max(size + thickness, posY), borderY - size - thickness);
        }
        protected int size = 20;
        public int Size { get { return size; } }

        protected static int thickness = 6;
        public static int Thickness { get { return thickness; } }

        protected Color color = Color.FromArgb(55, 53, 47);
        protected Color highlightColor = Color.FromArgb(107, 181, 255);

        public bool Selected { get; set; }

        public abstract void DrawItself(Graphics gfx);

        public abstract bool IfInside(int posX, int posY);

        public void ShiftPos(
            int shiftX, int shiftY, 
            int borderX, int borderY)
        {
            x = x + shiftX;
            y = y + shiftY;
            SetBoundedPos(x, y, borderX, borderY);
            //Debug.WriteLine($"CShape.ShiftPos({x}, {y})");
        }

        public void SwitchSelection()
        {
            Selected = Selected ? false : true;
        }

        public void Resize(int size, int borderX, int borderY)
        {
            Debug.WriteLine($"CShape.Resize({size}, {borderX}, {borderY})");
            this.size = size;
            SetBoundedPos(x, y, borderX, borderY);
        }
        public void FitNewCanvasSize(int borderX, int borderY)
        {
            SetBoundedPos(x, y, borderX, borderY);
        }
    }
}
