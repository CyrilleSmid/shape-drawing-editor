using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    internal class CEquilateralTriangle : CBasicShape
    {
        public CEquilateralTriangle(int posX, int posY) 
            : base(posX, posY) { }
        public CEquilateralTriangle() { }
        protected override void SetBoundedPos(int posX, int posY) 
        {
            int a = (int)(size * 2 / Math.Sqrt(3));
            x = Math.Min(Math.Max(size + thickness, posX), CanvasSizeX - size - thickness);
            y = Math.Min(Math.Max(a / 2 + thickness, posY), CanvasSizeY - a - thickness);
        }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            using (var brush = new SolidBrush(FillColor))
            {
                int a = (int)(size * 2 / Math.Sqrt(3));
                PointF[] triangleNodes =
                    {
                        new PointF(x, y + a),
                        new PointF(x - size, y - a / 2),
                        new PointF(x + size, y - a / 2)
                    };
                gfx.FillPolygon(brush, triangleNodes);
                gfx.DrawPolygon(Selected ? selectedPen : defaultPen, triangleNodes);
            }

        }

        public override bool IfInside(int posX, int posY)
        {
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= size * 0.7;
        }
        public override void Save(List<string> fileLines)
        {
            fileLines.Add("Shape type:");
            fileLines.Add("Triangle");
            SaveCommonProperties(fileLines);
        }
        public override void Load(Queue<string> fileLinesQueue)
        {
            LoadCommonProperties(fileLinesQueue);
        }
    }
}
