using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;

namespace oop_lab6_8.ShapeCommands
{
    public abstract class CShapeCommand
    {
        public abstract void Execute(CShape shape);
        public abstract void Unexecute();
        public abstract CShapeCommand Clone();
    }
}
