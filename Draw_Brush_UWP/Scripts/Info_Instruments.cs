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

namespace Draw_Brush_UWP.Scripts
{
    class Info_Instruments
    {
        static private Color color = Colors.Green;//цвет для рисования
        static private int type;//тип инструмента
        static private Color colorEraser = Colors.White;//цвет стирательной резинки (по умолчанию - белый 
        //, но в случае с прозрачным фоном - прозрачный)

        static public Color colorInit
        {
            get { return color; }
            set
            {
                if (value != null)
                    color = value;
                else
                    throw new ArgumentNullException();
            }
        }

        static public Color erasercolorInit
        {
            get { return colorEraser; }
            set
            {
                if (value != null)
                    colorEraser = value;
                else
                    throw new ArgumentNullException();
            }
        }


        static public int typeInit
        {
            get { return type; }
            set
            {
                if (!value.Equals(null))
                    type = value;
                else
                    throw new ArgumentNullException();
            }
        }





    }
}
