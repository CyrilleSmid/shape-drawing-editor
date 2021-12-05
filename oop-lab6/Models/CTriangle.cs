using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6.Models
{
    internal class CTriangle : CShape
    {
        public CTriangle(int posX, int posY, int borderX, int borderY) 
            : base(posX, posY, borderX, borderY) { }

        protected override void SetBoundedPos(int posX, int posY, int borderX, int borderY) 
        {
            int a = (int)(size * 2 / Math.Sqrt(3));
            x = Math.Min(Math.Max(size + thickness, posX), borderX - size - thickness);
            y = Math.Min(Math.Max(a / 2 + thickness, posY), borderY - a - thickness);
        }

        public override void DrawItself(Graphics gfx)
        {
            using (var defaultPen = new System.Drawing.Pen(color, thickness))
            using (var selectedPen = new System.Drawing.Pen(highlightColor, thickness))
            {
                int a = (int)(size * 2 / Math.Sqrt(3));
                PointF[] triangleNodes = 
                    {
                        new PointF(x, y + a),
                        new PointF(x - size, y - a / 2),
                        new PointF(x + size, y - a / 2)
                    };
                gfx.DrawPolygon(Selected ? selectedPen : defaultPen, triangleNodes);
            }

        }

        public override bool IfInside(int posX, int posY)
        {
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= size;
        }
    }
}
