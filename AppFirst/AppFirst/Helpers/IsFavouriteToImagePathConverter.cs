using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;

namespace BookingManagementSystem.Helpers;

public class IsFavouriteToImagePathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isFavourite)
        {
            var imagePath = isFavourite ? "/Assets/symbol-icons/favourite.png" : "/Assets/symbol-icons/not-favourite.png";
            //return new BitmapImage(new Uri("ms-appx://" + imagePath));
            var bitmapImage = new BitmapImage(new Uri("ms-appx://" + imagePath))
            {
                DecodePixelHeight = 20
            };
            return bitmapImage;
        }

        return new BitmapImage(new Uri("ms-appx:///Assets/symbol-icons/favourite.png"))
        {
            DecodePixelHeight = 20
        };
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is BitmapImage bitmapImage)
        {
            return !bitmapImage.UriSource.AbsoluteUri.Contains("not");
        }

        return false;
    }
}
