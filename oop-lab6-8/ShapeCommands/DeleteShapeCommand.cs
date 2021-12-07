using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;

namespace oop_lab6_8.ShapeCommands
{
    public class DeleteShapeCommand : CShapeCommand
    {
        CShape receiver;
        Container<CShape> shapeContainer;
        public DeleteShapeCommand(Container<CShape> shapeContainer)
        {
            this.shapeContainer = shapeContainer;
        }
        public override CShapeCommand Clone()
        {
            CShapeCommand command = new DeleteShapeCommand(shapeContainer);
            return command;
        }

        public override void Execute(CShape shape)
        {
            receiver = shape;
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

        public override void Unexecute()
        {
            shapeContainer.Append(receiver);
        }
    }
}
