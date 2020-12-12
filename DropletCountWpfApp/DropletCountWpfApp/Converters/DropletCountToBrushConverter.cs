using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CommonServiceLocator;
using DropletCountWpf.UI;
using DropletCountWpf.UI.ViewModel;

namespace DropletCountWpf.UI.Converters
{
    public class DropletCountToBrushConverter : IMultiValueConverter
    {
        private static ViewModel.MainViewModel MainVM => ServiceLocator.Current.GetInstance<ViewModel.MainViewModel>();
            

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {           
            var lowCountThreshold =  MainVM.DropletThreshold;           

            if (values[0].GetType() == typeof(string))
            {
                if ((values[1].ToString() != "-") && (values[0].ToString() != ""))
                {
                    var dropletCount = Int32.Parse(values[0].ToString());
                   // _wellCount++;
                    if (dropletCount < lowCountThreshold)
                    {                      

                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#A9A9A9")); // dark grey
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
