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
        private const int size = 16;
        public CTriangle(int posX, int posY)
        {
            x = posX;
            y = posY;
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
            return Math.Sqrt((x - posX) * (x - posX) + (y - posY) * (y - posY)) <= radius;
        }
    }
}
