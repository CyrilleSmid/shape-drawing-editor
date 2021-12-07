using oop_lab6_8.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop_lab6_8.Models;
using System.Diagnostics;
using oop_lab6_8.ShapeCommands;


namespace oop_lab6_8.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public IMainWindowView View { get; set; }
        public MainWindowViewModel(IMainWindowView view)
        {
            View = view;
            ShapeContainer.LoadShapes();
        }
        ~MainWindowViewModel()
        {
            ShapeContainer.SaveShapes();
        }

        SerializableShapeContainer _shapeContainer = new SerializableShapeContainer();
        public SerializableShapeContainer ShapeContainer 
        {
            get { return _shapeContainer; } 
            private set { _shapeContainer = value; }
        }

        private Stack<CShapeCommand> history = new Stack<CShapeCommand>(); // TODO implement delayed history commit

        public void UndoLastCommand()
        {
            if(history.Count > 0)
            {
                history.Pop().Unexecute();
            }
        }

        public void AddShape(int posX, int posY, Shapes shapeType)
        {
            CShape shape;
            switch(shapeType)
            {
                case Shapes.Square:
                    shape = new CSquare(posX, posY);
                    break;
                case Shapes.Triangle:
                    shape = new CEquilateralTriangle(posX, posY);
                    break;
                default:
                    shape = new CCircle(posX, posY);
                    break;
            }
            
            shape.Selected = true;
            SelectedShapeSize = shape.Size;
            SelectedShapeColor = System.Windows.Media.Color.FromArgb(
                shape.FillColor.A,
                shape.FillColor.R,
                shape.FillColor.G,
                shape.FillColor.B);

            AddShapeCommand addCommand = new AddShapeCommand(ShapeContainer);
            addCommand.Execute(shape);
            history.Push(addCommand);
        }
        public void SelectShapeAt(int posX, int posY)
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().IfInside(posX, posY))
                {
                    ShapeContainer.GetCurrent().SwitchSelection();
                }
            }
            UpdateSelectedShape();
        }
        private void UpdateSelectedShape()
        {
            bool anySelected = false;
            int newSelectedSize = 0;
            System.Drawing.Color newSelectedColor = new System.Drawing.Color();
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().Selected)
                {
                    if(anySelected || ShapeContainer.GetCurrent().GetType() == typeof(CShapeGroup))
                    {
                        return;
                    }
                    anySelected = true;
                    newSelectedSize = ShapeContainer.GetCurrent().Size;
                    newSelectedColor = ShapeContainer.GetCurrent().FillColor;
                }
            }
            SelectedShapeSize = newSelectedSize;
            SelectedShapeColor = System.Windows.Media.Color.FromArgb(
                newSelectedColor.A,
                newSelectedColor.R,
                newSelectedColor.G,
                newSelectedColor.B);
        }
        public void DeselectAllShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                ShapeContainer.GetCurrent().Selected = false;
            }
        }
        public void DeleteSelectedShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if(ShapeContainer.GetCurrent().Selected)
                {
                    DeleteShapeCommand deleteCommand = new DeleteShapeCommand(ShapeContainer);
                    deleteCommand.Execute(ShapeContainer.GetCurrent());
                    history.Push(deleteCommand);
                }
            }
        }
        public void ShiftSelectedShapes(int shiftX, int shiftY)
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected)
                {
                    ShiftShapeCommand shiftCommand = new ShiftShapeCommand(shiftX, shiftY);
                    shiftCommand.Execute(ShapeContainer.GetCurrent());
                    history.Push(shiftCommand);
                }
            }
        }

        private const int minSize = 10;
        private int maxSize;
        public int MinSize { get { return minSize; } }
        public int MaxSize 
        { 
            get { return maxSize; } 
            set 
            { 
                maxSize = value;
                OnPropertyChanged();
            } 
        }

        private int _selectedSize = 20;
        public int SelectedShapeSize
        {
            get { return _selectedSize; }
            set 
            {
                value = Math.Min(Math.Max(minSize, value), maxSize);
                if(_selectedSize != value)
                {
                    //Debug.WriteLine($"Size = {value}");
                    _selectedSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ResizeSelectedShapes()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected && 
                    ShapeContainer.GetCurrent().Size != SelectedShapeSize)
                {
                    ResizeShapeCommand resizeCommand = new ResizeShapeCommand(SelectedShapeSize);
                    resizeCommand.Execute(ShapeContainer.GetCurrent());
                    history.Push(resizeCommand);
                }
            }
        }
        public void CanvasSizeChanged()
        {
            CShape.CanvasSizeX = (int)View.GetCurentCanvasSize().X;
            CShape.CanvasSizeY = (int)View.GetCurentCanvasSize().Y;
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                ShapeContainer.GetCurrent().ReboundPosition();
            }
        }

        private System.Windows.Media.Color _selectedShapeColor = new System.Windows.Media.Color();
        private System.Windows.Media.Color SelectedShapeColor 
        {
            get { return _selectedShapeColor; }
            set 
            { 
                _selectedShapeColor = value;
                OnPropertyChanged(nameof(ShapeColorAlpha));
                OnPropertyChanged(nameof(ShapeColorRed));
                OnPropertyChanged(nameof(ShapeColorGreen));
                OnPropertyChanged(nameof(ShapeColorBlue));
            }
        }
        public byte ShapeColorAlpha
        {
            get { return SelectedShapeColor.A; }
            set
            {
                _selectedShapeColor.A = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorRed
        {
            get { return SelectedShapeColor.R; }
            set
            {
                _selectedShapeColor.R = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorGreen
        {
            get { return SelectedShapeColor.G; }
            set
            {
                _selectedShapeColor.G = value;
                OnPropertyChanged();
            }
        }
        public byte ShapeColorBlue
        {
            get { return SelectedShapeColor.B; }
            set
            {
                _selectedShapeColor.B = value;
                OnPropertyChanged();
            }
        }

        public void ChangeSelectedShapesColor()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected && ((
                    ShapeContainer.GetCurrent().FillColor.A == SelectedShapeColor.A &&
                    ShapeContainer.GetCurrent().FillColor.R == SelectedShapeColor.R &&
                    ShapeContainer.GetCurrent().FillColor.G == SelectedShapeColor.G &&
                    ShapeContainer.GetCurrent().FillColor.B == SelectedShapeColor.B) == false))
                {
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(
                        SelectedShapeColor.A,
                        SelectedShapeColor.R,
                        SelectedShapeColor.G,
                        SelectedShapeColor.B);
                    ChangeColorCommand changeColorCommand = new ChangeColorCommand(newColor);
                    changeColorCommand.Execute(ShapeContainer.GetCurrent());
                    history.Push(changeColorCommand);
                }
            }
        }

        public void GroupSelectedShapes()
        {
            GroupShapesCommand groupCommand = new GroupShapesCommand(ShapeContainer);
            CShapeGroup shapeGroup = new CShapeGroup();
            groupCommand.Execute(shapeGroup);
            history.Push(groupCommand);
        }
        public void UngroupSelection()
        {
            for (ShapeContainer.First();
                 ShapeContainer.IsEOL() == false;
                 ShapeContainer.Next())
            {
                if (ShapeContainer.GetCurrent().Selected &&
                    ShapeContainer.GetCurrent().GetType() == typeof(CShapeGroup))
                {
                    UngroupShapeCommand ungroupCommand = new UngroupShapeCommand(ShapeContainer);
                    CShapeGroup group = ((CShapeGroup)ShapeContainer.GetCurrent());
                    ungroupCommand.Execute(group);
                    history.Push(ungroupCommand);
                }
            }
        }
        public void Save()
        {
            ShapeContainer.SaveShapes();
        }
        public void Load()
        {
            ShapeContainer = new SerializableShapeContainer();
            ShapeContainer.LoadShapes();
        }
    }
}
