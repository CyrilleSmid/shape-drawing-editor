using oop_lab6_8.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.ShapeCommands
{
    internal class ResizeShapeCommand : CShapeCommand
    {
        CShape receiver;
        int newSize;
        int oldSize;
        public ResizeShapeCommand(int newSize)
        {
            this.newSize = newSize;
        }
        public override CShapeCommand Clone()
        {
            CShapeCommand command = new ResizeShapeCommand(newSize);
            return command;
        }

        public override void Execute(CShape shape)
        {
            receiver = shape;
            oldSize = shape.Size;
            shape.Resize(newSize);
        }

        public override void Unexecute()
        {
            receiver.Resize(oldSize);
        }
    }
}
