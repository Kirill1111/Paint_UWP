using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Draw_Brush_UWP.Scripts
{
    class action_Memory
    {
        static private List<List<UIElement>> list = new List<List<UIElement>>();

        static public void Next(List<UIElement> nextElement)
        {
            if (nextElement != null)
            {
                list.Add(  nextElement );
            }

        }

        static public void Next(UIElement nextElement)
        {
            if (nextElement != null)
            {
                list.Add( new List<UIElement>() { nextElement });
                return;
            }

        }

        static public List<UIElement> Back()
        {
            if (list.Count >= 1)
            {
                if (list.Count == 1)
                {
                    GC.Collect();
                }
                List<UIElement> backElement = list.ToList()[list.Count - 1];
                list.Remove(list.ToList()[list.Count - 1]);
                return backElement;
            }
            return new List<UIElement>();
            
        }

        static public void BackDelete(UIElement back)
        {
            if (list.Count >= 1)
            {
                List<UIElement> backElement = list.ToList()[list.Count - 1];
                list.Remove(new List<UIElement>() { back });
            }
        }


        static public void BackDelete(List<UIElement> back)
        {
            if (list.Count >= 1)
            {
                list.Remove( back );
            }
        }
    }
}
