using System.Collections.ObjectModel;
using AppFirst.Classes;
using AppFirst.Models;
using AppFirst.ViewModels.Pages;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace AppFirst.ViewModels.Dialogs
{
    public partial class UserDialogViewModel : ObservableObject
    {
        public LoadSqlDataSqliteViewModel loadSqlDataSqliteViewModel;

        private string _username;
        private string _password;

        private string _exampleProperty;

        public UserDialogViewModel()
        {

            _username = string.Empty;
            _password = string.Empty;
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        [ObservableProperty]
        private int _idLoginImage;

        [ObservableProperty]
        private int _selectedIndexLoginImage;

        [ObservableProperty]
        private ObservableCollection<LoginImage> _tableLoginImages;

        [ObservableProperty]
        private bool _isAdmin = false;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private WriteableBitmap _imageSource = null;

        [ObservableProperty]
        private string _errorInfoBarMessage = string.Empty;

        [ObservableProperty]
        private bool _errorInfoBarIsOpen = false;

        [RelayCommand]
        public void OnUserNameTextBoxTextChanged()
        {
            // Clear the error if the user name field isn't empty.
            if (!string.IsNullOrEmpty(Username))
            {
                ErrorInfoBarMessage = string.Empty;
                ErrorInfoBarIsOpen = false;
            }
        }

        [RelayCommand]
        public void OnPasswordTextBoxPasswordChanged()
        {
            // Clear the error if the password field isn't empty.
            if (!string.IsNullOrEmpty(Password))
            {
                ErrorInfoBarMessage = string.Empty;
                ErrorInfoBarIsOpen = false;
            }
        }

        [RelayCommand]
        private void OnSelectLoginImage()
        {
            if (SelectedIndexLoginImage >= 0)
            {
                ImageSource = TableLoginImages[SelectedIndexLoginImage].ImageSource;
                IdLoginImage = TableLoginImages[SelectedIndexLoginImage].Id;
            }
        }

        [RelayCommand]
        private async void OnAddLoginImage()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            var window = App.MainWindow;

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".bmp");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var bitmapImage =  await LoadImageFileToWriteableBitmap(file.Path);
                var loginImage = new LoginImage();
                loginImage.ImageName = file.Name;
                loginImage.Description = file.Path;
                loginImage.Image = ImageBlobConverter.ImageFilePathToBytes(file.Path);
                loginImage.ImageSource = bitmapImage;

                await loadSqlDataSqliteViewModel._loadSqlDataSqliteService_LoginImage.InsertLoginImageAsync(loginImage);

                TableLoginImages.Add(loginImage);
                SelectedIndexLoginImage = TableLoginImages.Count - 1;
                //await loadSqlDataSqliteViewModel.OnReloadLoginImages();
            }
            else
            {

            }

        }
        private async Task<WriteableBitmap> LoadImageFileToWriteableBitmap(string filePath)
        {
            var file = await StorageFile.GetFileFromPathAsync(filePath);
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var bitmapImage = new WriteableBitmap(1, 1);
                await bitmapImage.SetSourceAsync(stream);
                return bitmapImage;
            }
        }

    }
}
