using AppFirst.ViewModels.Dialogs;

namespace AppFirst.Views.Dialogs
{
    public sealed partial class LoginDialog : ContentDialog
    {
        Dictionary<string, string> _users;
        string ResultUsername { get; set; }
        public LoginDialogViewModel ViewModel { get; }
        public LoginDialog(Dictionary<string, string> users)
        {
            ViewModel = App.GetService<LoginDialogViewModel>();
            DataContext = ViewModel;

            this.InitializeComponent();

            _users = users;
            ResultUsername = string.Empty;

            this.PrimaryButtonClick += ContentDialog_PrimaryButtonClick;
            this.CloseButtonClick += ContentDialog_CloseButtonClick;
        }

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            if (!_users.ContainsKey(username))
                return false;

            if ((password) != _users[username])
                return false;

            return true;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text))
            {
                errorInfoBar.Message = "User name is required.";
                errorInfoBar.IsOpen = true;
            }
            else if (string.IsNullOrEmpty(passwordTextBox.Password))
            {
                errorInfoBar.Message = "Password is required.";
                errorInfoBar.IsOpen = true;
            }
            args.Cancel = errorInfoBar.IsOpen;

            if (args.Cancel == false)
            {
                bool isValidUser = ValidateCredentials(ViewModel.Username, ViewModel.Password);

                if (isValidUser)
                {
                    ResultUsername = ViewModel.Username;
                }
                else
                {
                    errorInfoBar.Message = "Invalid username or password";
                    errorInfoBar.IsOpen = true;
                    args.Cancel = true;
                }
            }

            if (args.Cancel == false)
            {
                ContentDialogButtonClickDeferral deferral = args.GetDeferral();
                if (await SimulateAsyncSignInOperation())
                {
                    //this.Result = SignInResult.SignInOK;
                }
                else
                {
                    //this.Result = SignInResult.SignInFail;
                }
                deferral.Complete();
            }
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Clear the error if the user name field isn't empty.
            if (!string.IsNullOrEmpty(userNameTextBox.Text))
            {
                errorInfoBar.Message = string.Empty;
                errorInfoBar.IsOpen = false;
            }
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Clear the error if the password field isn't empty.
            if (!string.IsNullOrEmpty(passwordTextBox.Password))
            {
                errorInfoBar.Message = string.Empty;
                errorInfoBar.IsOpen = false;
            }
        }


        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = false;
        }

        private async Task<bool> SimulateAsyncSignInOperation()
        {
            return true;
        }
    }
}
