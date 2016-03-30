using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FoodPantry
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = value as FoodPantrySheet;
            return new SolidColorBrush(d.HasError ? Colors.Red : Colors.Black);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new Exception();
        }
    }
}
