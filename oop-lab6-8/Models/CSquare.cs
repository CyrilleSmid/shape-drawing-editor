using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    internal class CSquare : CBasicShape
    {
        public CSquare(int posX, int posY)
            : base(posX, posY) { }
        public CSquare() { }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            using (var brush = new SolidBrush(FillColor))
            {
                gfx.FillRectangle(brush,
                x - size,
                y - size,
                size * 2,
                size * 2);
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
        public override void Save(List<string> fileLines)
        {
            fileLines.Add("Shape type:");
            fileLines.Add("Square");
            SaveCommonProperties(fileLines);
        }
        public override void Load(Queue<string> fileLinesQueue)
        {
            LoadCommonProperties(fileLinesQueue);
        }
    }
}
