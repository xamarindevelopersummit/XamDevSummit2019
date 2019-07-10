using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MSC.CM.XaSh.Converters
{
    public class BoolToHeartColorConverter : IValueConverter
    {
        public Color FalseColor = new Color(115, 115, 115); //microsoft gray
        public Color TrueColor = new Color(242, 80, 34);  //microsoft red
        private string falseColorStr = "#737373";
        private string trueColorStr = "#F25022";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool)value ? trueColorStr : falseColorStr;
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}