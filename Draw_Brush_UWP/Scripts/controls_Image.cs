using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Draw_Brush_UWP.Scripts
{
    class controls_Image
    {
        public static StackPanel Create(string Name, string Type)
        {
            Image myImage3 = CreateImg(Name,Type);

            StackPanel stackPnl = new StackPanel();
            stackPnl.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPnl.VerticalAlignment = VerticalAlignment.Stretch;
            stackPnl.Margin = new Thickness(-10, -6, -10, -6);
            stackPnl.Children.Add(myImage3);

            return stackPnl;
        }

        private static Image CreateImg(string Name, string Type)
        {
            Image myImage3 = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.UriSource = new Uri("ms-appx:///Images/" + Name + Type + ".png");
            myImage3.Stretch = Stretch.Fill;
            myImage3.Height = 75;
            myImage3.Source = bi3;

            return myImage3;
        }
        public static void UnActive(string Name,Grid InstrumentsGrid)
        {
            for (int i = 0; i < InstrumentsGrid.Children.Count; i++)
            {
                Button but = InstrumentsGrid.Children[i] as Button;
                if (Name != but.Name)
                {
                    StackPanel img = Scripts.controls_Image.Create(but.Name, "");
                    but.Content = img;
                }
                else
                    Scripts.Info_Instruments.typeInit = i + 1;
            }
        }

    }
}
