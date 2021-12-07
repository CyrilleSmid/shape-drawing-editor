using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace oop_lab6_8.Models
{
    public class SerializableShapeContainer : Container<CShape>
    {
        const string path = "PaintBoxSave.txt";
        public void SaveShapes()
        {
            List<string> fileLines = new List<string>();

            int shapeCount = 0;
            for (First(); IsEOL() == false; Next())
            {
                shapeCount += 1;
            }

            fileLines.Add("shapeCount:");
            fileLines.Add($"{shapeCount}");

            for (First(); IsEOL() == false; Next())
            {
                this.GetCurrent().Save(fileLines);
            }

            File.WriteAllLines(path, fileLines.ToArray());
        }

        public void LoadShapes()
        {
            Queue<string> fileLinesQueue = new Queue<string>();
            string[] fileLines = File.ReadAllLines(path);

            int shapeCount = int.Parse(fileLines[1]);

            for (int i = 3; i < fileLines.Length; i += 2)
            {
                fileLinesQueue.Enqueue(fileLines[i]);
            }
            while(fileLinesQueue.Count > 0)
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
                }
                if(shape != null)
                {
                    shape.Load(fileLinesQueue);
                    Append(shape);
                }
                else
                {
                    throw new Exception("Save file is corrupted");
                }
                
            }
        }
    }
}
