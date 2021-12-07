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

        public CBasicShape(int posX, int posY)
        {
            SetBoundedPos(posX, posY);
        }
        public CBasicShape() { }

        protected virtual void SetBoundedPos(int posX, int posY)
        {
            x = Math.Min(Math.Max(size + thickness, posX), CanvasSizeX - size - thickness);
            y = Math.Min(Math.Max(size + thickness, posY), CanvasSizeY - size - thickness);
        }

        public override void ReboundPosition()
        {
            SetBoundedPos(x, y);
        }

        public override Point ShiftPos(int shiftX, int shiftY)
        {
            //Debug.WriteLine($"CShape.ShiftPos({x}, {y})");
            int oldPosX = x;
            int oldPosY = y;
            x = x + shiftX;
            y = y + shiftY;
            ReboundPosition();
            return new Point(x - oldPosX, y - oldPosY);
        }
        public override System.Drawing.Point ActualShift(int shiftX, int shiftY)
        {
            int newPotentialX = Math.Min(Math.Max(size + thickness, x + shiftX), CanvasSizeX - size - thickness);
            int newPotentialY = Math.Min(Math.Max(size + thickness, y + shiftY), CanvasSizeY - size - thickness);
            return new Point(newPotentialX - x, newPotentialY - y);
        }

        public override void SwitchSelection()
        {
            Selected = Selected ? false : true;
            //Debug.WriteLine($"CShape.SwitchSelection to {Selected}");
        }

        public override void Resize(int size)
        {
            //Debug.WriteLine($"CShape.Resize({size}, {CanvasSizeX}, {CanvasSizeY})");
            this.size = size;
            ReboundPosition();
        }

        protected void SaveCommonProperties(List<string> fileLines)
        {
            fileLines.Add("Position X:");
            fileLines.Add($"{x}");
            fileLines.Add("Position Y:");
            fileLines.Add($"{y}");

            fileLines.Add("Size:");
            fileLines.Add($"{size}");

            fileLines.Add("Color A:");
            fileLines.Add($"{FillColor.A}");
            fileLines.Add("Color R:");
            fileLines.Add($"{FillColor.R}");
            fileLines.Add("Color G:");
            fileLines.Add($"{FillColor.G}");
            fileLines.Add("Color B:");
            fileLines.Add($"{FillColor.B}");
        }
        protected void LoadCommonProperties(Queue<string> fileLinesQueue)
        {
            x = int.Parse(fileLinesQueue.Dequeue());
            y = int.Parse(fileLinesQueue.Dequeue());

            size = int.Parse(fileLinesQueue.Dequeue());

            Color fillColor = Color.FromArgb(
                int.Parse(fileLinesQueue.Dequeue()),
                int.Parse(fileLinesQueue.Dequeue()),
                int.Parse(fileLinesQueue.Dequeue()),
                int.Parse(fileLinesQueue.Dequeue())
                );
            FillColor = fillColor;
        }
    }
}
