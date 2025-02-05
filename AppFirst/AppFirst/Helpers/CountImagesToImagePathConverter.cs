using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;

namespace AppFirst.Helpers;

public class CountImagesToImagePathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int countImages)
        {
            var imagePath = (countImages > 0) ? "/Assets/cloud-download-outline.png" : "/Assets/cloud-offline-outline.png";
            var bitmapImage = new BitmapImage(new Uri("ms-appx://" + imagePath))
            {
                DecodePixelHeight = 50
            };
            return bitmapImage;
        }

        return new BitmapImage(new Uri("ms-appx:///Assets/cloud-download-outline.png"))
        {
            DecodePixelHeight = 50
        };
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is BitmapImage bitmapImage)
        {
            if (!bitmapImage.UriSource.AbsoluteUri.Contains("offline"))
            {
                return 1;
            }
        }

        return 0;
    }
}
