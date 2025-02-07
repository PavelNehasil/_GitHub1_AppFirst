using AppFirst.Models;

namespace AppFirst.Views.Dialogs
{
    public sealed partial class ContentDialogDeleteImage : ContentDialog
    {
        private LoginImage _loginImage = null;
        public ContentDialogDeleteImage()
        {
            this.InitializeComponent();

        }

        public void SetLoginImage(LoginImage loginImage)
        {
            _loginImage = loginImage;

            TextContentTBl.Text = $"Do you want delete image Id({(loginImage.Id)}) Name({loginImage.ImageName}) from database?";
            DeletedImageImg.Source = loginImage.ImageSource;
        }

        private void DeleteButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
