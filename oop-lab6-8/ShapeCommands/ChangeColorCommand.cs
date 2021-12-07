using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;

namespace oop_lab6_8.ShapeCommands
{
    internal class ChangeColorCommand : CShapeCommand
    {
        CShape receiver;
        System.Drawing.Color newColor;
        System.Drawing.Color oldColor;
        public ChangeColorCommand(System.Drawing.Color newColor)
        {
            this.newColor = newColor;
        }
        public override CShapeCommand Clone()
        {
            CShapeCommand command = new ChangeColorCommand(newColor);
            return command;
        }

        public override void Execute(CShape shape)
        {
            receiver = shape;
            oldColor = shape.FillColor;
            shape.FillColor = newColor;
        }

        public override void Unexecute()
        {
            receiver.FillColor = oldColor;
        }
    }
}
