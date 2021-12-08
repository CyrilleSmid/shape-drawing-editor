using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.Models
{
    internal class CShapeGroup : CShape
    {
        Container<CShape> shapeGroup = new Container<CShape>();

        public override Color FillColor
        {
            get
            {
                shapeGroup.First();
                return shapeGroup.GetCurrent().FillColor;
            }
            set
            {
                for (shapeGroup.First();
                   shapeGroup.IsEOL() == false;
                   shapeGroup.Next())
                {
                    shapeGroup.GetCurrent().FillColor = value;
                }
            }
        }

        private bool _selected = true;
        public override bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                for (shapeGroup.First();
                   shapeGroup.IsEOL() == false;
                   shapeGroup.Next())
                {
                    shapeGroup.GetCurrent().Selected = value;
                }
            }

        }

        public override int Size
        {
            get
            {
                shapeGroup.First();
                return shapeGroup.GetCurrent().Size;
            }
        }

        public override void DrawItself(Graphics gfx)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().DrawItself(gfx);
            }
        }

        public override bool IfInside(int posX, int posY)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                if (shapeGroup.GetCurrent().IfInside(posX, posY))
                {
                    return true;
                }
            }
            return false;
        }

        public override Point ShiftPos(int shiftX, int shiftY)
        {
            Point actualShift = ActualShift(shiftX, shiftY);
            for (shapeGroup.First();
            shapeGroup.IsEOL() == false;
            shapeGroup.Next())
            {
                shapeGroup.GetCurrent().ShiftPos(actualShift.X, actualShift.Y);
            }
            return new Point(actualShift.X, actualShift.Y); 
        }
        public override Point ActualShift(int shiftX, int shiftY)
        {
            int actualShiftX = int.MaxValue;
            int actualShiftY = int.MaxValue;
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                Point actualShift = shapeGroup.GetCurrent().ActualShift(shiftX, shiftY);
                if(Math.Abs(actualShift.X) < Math.Abs(actualShiftX))
                {
                    actualShiftX = actualShift.X;
                }
                if (Math.Abs(actualShift.Y) < Math.Abs(actualShiftY))
                {
                    actualShiftY = actualShift.Y;
                }
            }
            return new Point(actualShiftX, actualShiftY);
        }

        public override void SwitchSelection()
        {
            Selected = Selected ? false : true;
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().Selected = Selected;
            }
        }

        public override void Resize(int size)
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().Resize(size);
            }
        }
        public override void ReboundPosition()
        {
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().ReboundPosition();
            }
        }
        public void GroupSelectedShapes(Container<CShape> ShapeContainer)
        {
            // TODO: if only one group is selected
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    shapeGroup.Append(ShapeContainer.GetCurrent());
                    ShapeContainer.DeleteCurrent();
                }
            }
            Selected = true;
            ShapeContainer.Append(this);
        }
        public List<CShape> UngroupShapes(Container<CShape> shapeContainer)
        {
            List<CShape> ungroupedShapes = new List<CShape>();
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeContainer.Append(shapeGroup.GetCurrent());
                ungroupedShapes.Add(shapeGroup.GetCurrent());
                shapeGroup.DeleteCurrent();
            }
            shapeContainer.SaveIterationState();
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                if(shapeContainer.GetCurrent() == this)
                {
                    shapeContainer.DeleteCurrent();
                }
            }
            shapeContainer.RevertToSavedIterationState(true);
            return ungroupedShapes;
        }
        public override void Save(List<string> fileLines)
        {
            fileLines.Add("Shape type:");
            fileLines.Add("ShapeGroup");

            int shapeCount = 0;
            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeCount += 1;
            }
            fileLines.Add("shapeCount:");
            fileLines.Add($"{shapeCount}");

            for (shapeGroup.First();
                 shapeGroup.IsEOL() == false;
                 shapeGroup.Next())
            {
                shapeGroup.GetCurrent().Save(fileLines);
            }
        }
        public override void Load(Queue<string> fileLinesQueue)
        {
            int shapeCount = int.Parse(fileLinesQueue.Dequeue());

            for (int i = 0; i < shapeCount; i++)
            {
                string shapeType = fileLinesQueue.Dequeue();
                CShape shape = null;
                switch (shapeType)
                {
                    case "Circle":
                        shape = new CCircle();
                        break;
                    case "Square":
                        shape = new CSquare();
                        break;
                    case "Triangle":
                        shape = new CEquilateralTriangle();
                        break;
                    case "ShapeGroup":
                        shape = new CShapeGroup();
                        break;
                }
                if (shape != null)
                {
                    shape.Load(fileLinesQueue);
                    shapeGroup.Append(shape);
                }
                else
                {
                    throw new Exception("Save file is corrupted");
                }
            }
            
        }
    }
}
