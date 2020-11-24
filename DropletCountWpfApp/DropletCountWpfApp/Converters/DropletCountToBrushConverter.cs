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
using DropletCountWpf.App;
using DropletCountWpf.App.ViewModel;

namespace DropletCountWpf.App.Converters
{
    public class DropletCountToBrushConverter : IMultiValueConverter
    {
        private MainViewModel MainVM => ServiceLocator.Current.GetInstance<MainViewModel>();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //  string lowCountThreashold = parameter as string;

            var lowCountThreshold = Int32.Parse(MainVM.DropletThreshold);


            if (values[0].GetType() == typeof(string))
            {            

                if ((values[1].ToString() != "Bin") && (values[0].ToString() != ""))
                {
                    var dropletCount = Int32.Parse(values[0].ToString());
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
