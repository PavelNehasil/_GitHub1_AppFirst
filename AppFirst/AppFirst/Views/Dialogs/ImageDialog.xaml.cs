
using Microsoft.UI.Xaml.Media.Imaging;

namespace AppFirst.Views.Dialogs;

public sealed partial class ImageDialog : ContentDialog
{
    public ImageDialog()
    {
        this.InitializeComponent();
    }

    public void SetImage(string imageName, WriteableBitmap image, int width, int height)
    {
        Title = imageName;
        ImageImage.Source = image;
        ImageImage.Width = width;
        ImageImage.Height = height;
    }
}
