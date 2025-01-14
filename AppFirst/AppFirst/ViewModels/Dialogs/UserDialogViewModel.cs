using System.Collections.ObjectModel;
using AppFirst.Models;
using Microsoft.UI.Xaml.Media.Imaging;

namespace AppFirst.ViewModels.Dialogs
{
    public partial class UserDialogViewModel : ObservableObject
    {

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
        private int _selectedItemLoginImage;

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
        private BitmapImage _imageSource = null;

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

    }
}
