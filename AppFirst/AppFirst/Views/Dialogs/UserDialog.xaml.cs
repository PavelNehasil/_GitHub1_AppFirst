using AppFirst.Models;
using AppFirst.ViewModels.Dialogs;

namespace AppFirst.Views.Dialogs
{
    public sealed partial class UserDialog : ContentDialog
    {
        List<string> _users;
        public User ResultUser { get; set; }
        public UserDialogViewModel ViewModel { get; }
        public UserDialog(List<string> users)
        {
            ViewModel = App.GetService<UserDialogViewModel>();
            DataContext = ViewModel;

            this.InitializeComponent();

            _users = users;
            ResultUser = null;

            this.PrimaryButtonClick += ContentDialog_PrimaryButtonClick;
            this.CloseButtonClick += ContentDialog_CloseButtonClick;
        }

        public void SetUser(User user)
        {
            ViewModel.Username = user.UserName;
            ViewModel.Password = user.Password;
            ViewModel.IsAdmin = user.IsAdmin;
            ViewModel.Email = user.Email;
            ViewModel.FirstName = user.FirstName;
            ViewModel.LastName = user.LastName;
        }

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            if (0 < _users.IndexOf(username))
                return false;

            return true;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (-1 > _users.IndexOf(ViewModel.Username))
            {
                errorInfoBar.Message = "User name already exists.";
                errorInfoBar.IsOpen = true;
            }
            else if (string.IsNullOrEmpty(userNameTextBox.Text))
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
                //var verified = BC.Verify("MyPassword", user.Password);
                ResultUser = new User();
                ResultUser.UserName = ViewModel.Username;
                ResultUser.Password = ViewModel.Password;//BCrypt.Net.BCrypt.HashPassword(ViewModel.Password);
                ResultUser.IsAdmin = ViewModel.IsAdmin;
                ResultUser.Email = ViewModel.Email;
                ResultUser.FirstName = ViewModel.FirstName;
                ResultUser.LastName = ViewModel.LastName;
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
