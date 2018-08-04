using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Diagnostics;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;
using Windows.System;
using Windows.UI.Core;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Draw_Brush_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Point start;
        Point end;
        Line line;
        Line newLine;

        private bool isPaint = false;
        private bool isExit = false;

        private  int SIZE = 4;

        private Line EndLine = new Windows.UI.Xaml.Shapes.Line();

        private List<UIElement> PencilList = new List<UIElement>();
        private List<UIElement> EraserList = new List<UIElement>();

        public void Draw(PointerRoutedEventArgs e)
        {
            switch (Scripts.Info_Instruments.typeInit)
            {
                case 0x1:
                    DrawLine(e);
                    break;
                case 0x2:
                    DrawPencil(e);
                    break;
                case 0x3:
                    DrawEraser(e);
                    break;
            }
        }

        private void DrawEraser(PointerRoutedEventArgs e)
        {
            if (TestLeftMouseClick(e))
            {
                if (!isPaint) return;
                    var point = e.GetCurrentPoint(canvas).Position;
                    var line = new Line
                    {
                        Stroke = new SolidColorBrush(Draw_Brush_UWP.Scripts.Info_Instruments.erasercolorInit),
                        StrokeThickness = SIZE,
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = point.X,
                        Y2 = point.Y,
                        StrokeStartLineCap = PenLineCap.Round,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    start = point;
                    canvas.Children.Add(line);

                EraserList.Add(line);
            }
        }

        private void DrawPencil(PointerRoutedEventArgs e)
        {
            if (TestLeftMouseClick(e))
            {
                if (!isPaint) return;
                    var point = e.GetCurrentPoint(canvas).Position;
                    var line = new Line
                    {
                        Stroke = new SolidColorBrush(Scripts.Info_Instruments.colorInit),
                        StrokeThickness = SIZE,
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = point.X,
                        Y2 = point.Y,
                        StrokeStartLineCap = PenLineCap.Round,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    start = point;
                canvas.Children.Add(line);
                PencilList.Add(line);
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            Scripts.Info_Instruments.colorInit = Colors.Black;
        }

        private void canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PencilList = new List<UIElement>();
            EraserList = new List<UIElement>();

            if (TestLeftMouseClick(e))
            {
                start = e.GetCurrentPoint(canvas).Position;
                    end = start;
                       line = null;
                switch (Scripts.Info_Instruments.typeInit)
                {
                    case 2:
                        if (isPaint == true) return;
                        Draw(e);
                        isPaint = true;
                        var line1 = new Line
                        {
                            Stroke = new SolidColorBrush(Scripts.Info_Instruments.colorInit),
                            StrokeThickness = SIZE,
                            X1 = start.X,
                            Y1 = start.Y,
                            X2 = start.X,
                            Y2 = start.Y,
                            StrokeStartLineCap = PenLineCap.Round,
                            StrokeEndLineCap = PenLineCap.Round
                        };
                        canvas.Children.Add(line1);

                        PencilList.Add(line1);
                        break;
                    case 3:
                        if (isPaint) return;
                        Draw(e);
                        isPaint = true;
                        var line2 = new Line
                        {
                            Stroke = new SolidColorBrush(Scripts.Info_Instruments.erasercolorInit),
                            StrokeThickness = SIZE,
                            X1 = start.X,
                            Y1 = start.Y,
                            X2 = start.X,
                            Y2 = start.Y,
                            StrokeStartLineCap = PenLineCap.Round,
                            StrokeEndLineCap = PenLineCap.Round
                        };
                        canvas.Children.Add(line2);

                       EraserList.Add(line2);
                        break;
                }
            }

        }

        private void canvas_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            start = e.GetCurrentPoint(canvas).Position;
            end = start;
            switch (Scripts.Info_Instruments.typeInit)
            {
                case 1:
                    if(isExit)
                    Scripts.action_Memory.BackDelete(EndLine);
                    Scripts.action_Memory.Next(line);
                    line = null;
                    break;
                case 2:
                    isPaint = false;
                    Scripts.action_Memory.Next(PencilList);
                    PencilList = null;
                    break;
                case 3:
                    isPaint = false;
                    Scripts.action_Memory.Next(EraserList);
                    EraserList = null;
                    break;
            }

            Draw(e);
        }

        private void canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if( TestLeftMouseClick(e))
            {

                end = e.GetCurrentPoint(canvas).Position;
                canvas.Children.Remove(line);

                Draw(e);

                if (Scripts.Info_Instruments.typeInit == 2 || Scripts.Info_Instruments.typeInit == 3)
                {
                    isPaint = true;
                    start = e.GetCurrentPoint(canvas).Position;
                }
            }
        }

        private void Draw_Line(PointerRoutedEventArgs e)
        {
            Double X1 = start.X;
            Double Y1 = start.Y;
            Double X2 = end.X;//Присвоить X2 значение X текущей точки
            Double Y2 = end.Y;//Присвоить X2 значение Y текущей точки

            if (e.KeyModifiers == Windows.System.VirtualKeyModifiers.Shift)//Если нажата клавиши shift
            {
                if (Math.Abs(Math.Abs(end.Y - start.Y) - Math.Abs(end.X - start.X)) < 40)
                {
                    if (end.X > start.X)
                        X2 = X1 + Math.Abs(end.Y - start.Y);
                    else if (end.X < start.X)
                        X2 = X1 - Math.Abs(end.Y - start.Y);
                }
                else if (Math.Abs(end.Y - start.Y) > Math.Abs(end.X - start.X))
                {
                    X2 = start.X;
                    Y2 = end.Y;
                }
                else if (Math.Abs(end.Y - start.Y) < Math.Abs(end.X - start.X))
                {
                    X2 = end.X;
                    Y2 = start.Y;
                }
            }

            newLine = new Line()//Отрисовать линию
            {
                Stroke = new SolidColorBrush(Scripts.Info_Instruments.colorInit),
                StrokeThickness = SIZE,
                X1 = X1,
                Y1 = Y1,
                X2 = X2,
                Y2 = Y2
            };

            line = newLine;
            canvas.Children.Add(newLine);
        }

        // Sets and draws line after mouse is released
        private void DrawLine(PointerRoutedEventArgs e)
        {
            Draw_Line(e);
        }

        private void ButClick(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            string Name = but.Name;

            StackPanel myImage3 = Scripts.controls_Image.Create(but.Name, "Active");
            but.Content = myImage3;
            Scripts.controls_Image.UnActive(Name,InstrumentsGrid);
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            text.Text = slider.Value.ToString();
            SIZE =(int)slider.Value;
        }

        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if(isPaint == true)
            isPaint = false;
            Debug.WriteLine("1");
        }

        private bool TestLeftMouseClick(PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;

            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(this);
                if (ptrPt.Properties.IsLeftButtonPressed)
                {
                    return true;
                }
            }
            return false;
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var ctrl = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down);

            if (ctrl && e.Key ==  Windows.System.VirtualKey.C)
            {
                List<UIElement> UI = Scripts.action_Memory.Back();

                foreach (UIElement i in UI)
                {
                    canvas.Children.Remove(i);
                }
            }
        }

        private void canvas_PointerExited_1(object sender, PointerRoutedEventArgs e)
        {
            if (TestLeftMouseClick(e))
            {
                switch (Scripts.Info_Instruments.typeInit)
                {
                    case 1:
                        Scripts.action_Memory.BackDelete(EndLine);
                        Scripts.action_Memory.Next(line);
                        isExit = true;
                        EndLine = line;
                        break;

                }
            }
        }
    }
}

