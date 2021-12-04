using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_lab6.Models
{
    public class Container<T> where T : class
    {
        private T[] elements;
        private int curElIndex;
        private int lastElIndex = -1;

        public Container()
        {
            //Console.WriteLine("Debug: Container - default constructor");
            elements = new T[0];
        }
        public Container(Container<T> container)
        {
            //Console.WriteLine("Debug: Container - copy constructor");
            this.elements = container.elements;
        }
        ~Container()
        {
            //Console.WriteLine("Debug: Container - destructor");
        }
        public void Append(T element)
        {
            //Console.WriteLine("Debug: Container.Append()");

            if (lastElIndex == elements.Length - 1)
            {
                ExpandArrayAndCopyContents();
            }
            elements[lastElIndex + 1] = element;
            lastElIndex += 1;
        }
        public void Insert(T element)
        {
            //Console.WriteLine("Debug: Container.Insert()");

            if (lastElIndex == elements.Length - 1)
            {
                ExpandArrayAndCopyContents();
            }
            for (int i = lastElIndex; i >= curElIndex; i--)
            {
                elements[i + 1] = elements[i];
            }
            elements[curElIndex] = element;
            lastElIndex += 1;
        }
        private void ExpandArrayAndCopyContents()
        {
            T[] newArrayExpanded = new T[elements.Length + 10]; 
            for (int i = 0; i < elements.Length; i++)
            {
                newArrayExpanded[i] = elements[i];
            }
            elements = newArrayExpanded;
        }
        private bool shiftedByDeletion = false;
        public void DeleteCurrent()
        {
            //Console.WriteLine("Debug: Container.DeleteCurrent()");
            if (elements.Length != 0)
            {
                for (int i = curElIndex; i < lastElIndex; i++)
                {
                    elements[i] = elements[i + 1];
                }
                elements[lastElIndex] = null;
                lastElIndex -= 1;
                shiftedByDeletion = true;
            }

        }
        public void First()
        {
            //Console.WriteLine("Debug: Container.First()");
            curElIndex = 0;
        }
        public void Next()
        {
            //Console.WriteLine("Debug: Container.Next()");
            if (shiftedByDeletion == false)
            {
                curElIndex += 1;
            }
            shiftedByDeletion = false;
        }
        public T GetCurrent()
        {
            //Console.WriteLine("Debug: Container.GetCurrent()");
            return elements[curElIndex];
        }
        public bool IsEOL()
        {
            //Console.WriteLine("Debug: Container.IsEOL()");
            if (curElIndex > lastElIndex)
                return true;

            return false;
        }
    }
}
