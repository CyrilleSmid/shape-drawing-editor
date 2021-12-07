using oop_lab6_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6_8.ShapeCommands
{
    public class ShiftShapeCommand : CShapeCommand
    {
        CShape receiver;
        int shiftX;
        int shiftY;
        public ShiftShapeCommand(int shiftX, int shiftY)
        {
            this.shiftX = shiftX;
            this.shiftY = shiftY;
        }
        public override CShapeCommand Clone()
        {
            CShapeCommand command = new ShiftShapeCommand(shiftX, shiftY);
            return command;
        }

        public override void Execute(CShape shape)
        {
            receiver = shape;
            shape.ShiftPos(shiftX, shiftY);
        }

        public override void Unexecute()
        {
            receiver.ShiftPos(-shiftX, -shiftY);
        }
    }
}
