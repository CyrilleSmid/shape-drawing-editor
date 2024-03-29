﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace oop_lab6_8.Models
{
    public enum Shapes
    {
        Circle,
        Square,
        Triangle
    }

    public abstract class CShape
    {
        public static int CanvasSizeX { get; set; }
        public static int CanvasSizeY { get; set; }

        public CShape() { }
    
        public abstract int Size { get; }

        public abstract Color FillColor { get; set; }

        public abstract bool Selected { get; set; }

        public abstract void DrawItself(Graphics gfx);

        public abstract bool IfInside(int posX, int posY);

        public abstract System.Drawing.Point ShiftPos(int shiftX, int shiftY);

        public abstract System.Drawing.Point ActualShift(int shiftX, int shiftY);

        public abstract void SwitchSelection();

        public abstract void Resize(int size);

        public abstract void ReboundPosition();

        public abstract void Save(List<string> fileLines);

        public abstract void Load(Queue<string> fileLinesQueue);
    }
}
