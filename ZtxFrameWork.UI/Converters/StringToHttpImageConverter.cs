using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ZtxFrameWork.UI.Converters
{
    public class StringToHttpImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //  return  new BitmapImage(new Uri($"http://124.232.147.213:88/{value.ToString()}.png"));
            return null;    
            return new BitmapImage(new Uri($"{App.HttpPath}/{value.ToString()}.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
