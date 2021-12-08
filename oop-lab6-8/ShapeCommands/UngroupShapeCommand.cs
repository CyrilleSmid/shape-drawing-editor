using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;

namespace oop_lab6_8.ShapeCommands
{
    internal class UngroupShapeCommand : CShapeCommand
    {
        CShapeGroup receiver;
        Container<CShape> shapeContainer;
        List<CShape> ungroupedShapes = new List<CShape>();
        public UngroupShapeCommand(Container<CShape> shapeContainer)
        {
            this.shapeContainer = shapeContainer;
        }

        public override CShapeCommand Clone()
        {
            return new UngroupShapeCommand(shapeContainer);
        }

        public override void Execute(CShape shapeGroup)
        {
            if (shapeGroup.GetType() == typeof(CShapeGroup))
            {
                receiver = (CShapeGroup)shapeGroup;
                ungroupedShapes = receiver.UngroupShapes(shapeContainer);
            }
        }

        public override void Unexecute()
        {
            if (receiver != null)
            {
                for (shapeContainer.First();
                     shapeContainer.IsEOL() == false;
                     shapeContainer.Next())
                {
                    if(ungroupedShapes.Contains(shapeContainer.GetCurrent()))
                    {
                        shapeContainer.GetCurrent().Selected = true;
                    }
                    else
                    {
                        shapeContainer.GetCurrent().Selected = false;
                    }
                }
                receiver.GroupSelectedShapes(shapeContainer);
            }
        }
    }
}
