using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;

namespace oop_lab6_8.ShapeCommands
{
    public class AddShapeCommand : CShapeCommand
    {
        CShape receiver;
        Container<CShape> shapeContainer;
        public AddShapeCommand(Container<CShape> shapeContainer)
        {
            this.shapeContainer = shapeContainer;
        }
        public override CShapeCommand Clone()
        {
            CShapeCommand command = new AddShapeCommand(shapeContainer);
            return command;
        }

        public override void Execute(CShape shape)
        {
            receiver = shape;
            shapeContainer.Append(shape);
        }

        public override void Unexecute()
        {
            shapeContainer.SaveIterationState();
            for (shapeContainer.First();
                 shapeContainer.IsEOL() == false;
                 shapeContainer.Next())
            {
                if (shapeContainer.GetCurrent() == receiver)
                {
                    shapeContainer.DeleteCurrent();
                }
            }
            shapeContainer.RevertToSavedIterationState(true);
        }
    }
}
