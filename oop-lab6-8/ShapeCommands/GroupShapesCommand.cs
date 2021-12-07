using oop_lab6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.ShapeCommands
{
    public class GroupShapesCommand : CShapeCommand
    {
        CShapeGroup receiver;
        Container<CShape> shapeContainer;
        public GroupShapesCommand(Container<CShape> shapeContainer)
        {
            this.shapeContainer = shapeContainer;
        }

        public override CShapeCommand Clone()
        {
            return new GroupShapesCommand(shapeContainer);
        }

        public override void Execute(CShape shapeGroup)
        {
            if(shapeGroup.GetType() == typeof(CShapeGroup))
            {
                receiver = (CShapeGroup)shapeGroup;
                receiver.GroupSelectedShapes(shapeContainer);
            }
        }

        public override void Unexecute()
        {
            if(receiver != null) // TODO: Group - Ungroup - Undo Ungroup - original group doesn't exist
            {
                receiver.UngroupShapes(shapeContainer);
            }
        }
    }
}
