using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    public abstract class CBasicShape : CShape
    {
        protected int x, y;
        protected int size = 20;
        protected const int thickness = 6;
        protected Color color = Color.FromArgb(0, 55, 53, 47);
        protected Color highlightColor = Color.FromArgb(107, 181, 255);
        protected Color fillColor = Color.FromArgb(255, 55, 53, 47);

        public override int Size { get { return size; } }

        public override Color FillColor { get { return fillColor; } set { fillColor = value; } }
        public override bool Selected { get; set; }

        public CBasicShape(int posX, int posY, int borderX, int borderY)
        {
            SetBoundedPos(posX, posY, borderX, borderY);
        }

        protected virtual void SetBoundedPos(int posX, int posY, int borderX, int borderY)
        {
            x = Math.Min(Math.Max(size + thickness, posX), borderX - size - thickness);
            y = Math.Min(Math.Max(size + thickness, posY), borderY - size - thickness);
        }

        public override void ReboundPosition(int borderX, int borderY)
        {
            SetBoundedPos(x, y, borderX, borderY);
        }

        public override void ShiftPos(
            int shiftX, int shiftY,
            int borderX, int borderY)
        {
            Debug.WriteLine($"CShape.ShiftPos({x}, {y})");
            x = x + shiftX;
            y = y + shiftY;
            SetBoundedPos(x, y, borderX, borderY);
        }

        public override void SwitchSelection()
        {
            Selected = Selected ? false : true;
            Debug.WriteLine($"CShape.SwitchSelection to {Selected}");
        }

        public override void Resize(int size, int borderX, int borderY)
        {
            Debug.WriteLine($"CShape.Resize({size}, {borderX}, {borderY})");
            this.size = size;
            SetBoundedPos(x, y, borderX, borderY);
        }

        public override void DrawItself(Graphics gfx)
        {
            throw new NotImplementedException();
        }

        public override bool IfInside(int posX, int posY)
        {
            throw new NotImplementedException();
        }
    }
}
