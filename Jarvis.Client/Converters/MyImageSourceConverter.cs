﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Jarvis.Client.Converters
{
    public class MyImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(ImageSource))
            {
                if (value is string)
                {
                    string str = (string)value;
                    return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
                }
                else if (value is Uri)
                {
                    Uri uri = (Uri)value;
                    return new BitmapImage(uri);
                }
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
