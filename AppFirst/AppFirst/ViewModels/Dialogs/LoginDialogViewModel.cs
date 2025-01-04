namespace AppFirst.ViewModels.Dialogs
{
    public partial class LoginDialogViewModel : ObservableObject
    {

        private string _username;
        private string _password;
        private string _exampleProperty;

        public LoginDialogViewModel()
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
