using Microsoft.UI.Xaml.Media.Imaging;
using WinUIEx;

namespace AppFirst.Views.Dialogs;

public sealed partial class ImageDialogEx : WindowEx
{
    public ImageDialogEx()
    {
        this.InitializeComponent();
    }

    public void SetImage(string imageName, BitmapImage image, int width, int height)
    {
        Title = imageName;
        ImageImage.Source = image;
        ImageImage.Width = width;
        ImageImage.Height = height;
    }

    private void OnClose(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
