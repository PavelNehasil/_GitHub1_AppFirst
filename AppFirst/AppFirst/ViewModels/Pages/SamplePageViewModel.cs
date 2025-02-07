using AppFirst.Views.Dialogs;
using WinUIEx;

namespace AppFirst.ViewModels.Pages;
public partial class SamplePageViewModel : ObservableObject
{
    ProgressDialogEx progressDialogEx;

    [RelayCommand]
    private async Task OnShowLoginDialog()
    {


        var users = new Dictionary<string, string>();
        users.Add("admin", "admin");
        users.Add("sa", "123");

        var loginDialog = new LoginDialog(users);
        loginDialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        //loginDialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        //loginDialog.Title = "Login dialog";
        //loginDialog.PrimaryButtonText = "Login";
        //loginDialog.CloseButtonText = "Cancel";
        loginDialog.DefaultButton = ContentDialogButton.Primary;
        //loginDialog.Content = contentDialogContent;



        var result = await loginDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            //DialogResult.Text = "User saved their work";
        }
        else if (result == ContentDialogResult.Secondary)
        {
            //DialogResult.Text = "User did not save their work";
        }
        else
        {
            //DialogResult.Text = "User cancelled the dialog";
        }
    }

    [RelayCommand]
    private void OnShowProgressDialogEx()
    {
        ShowProgressDialogExWindow();
    }

    public void ShowProgressDialogExWindow()
    {
        if (progressDialogEx is null || progressDialogEx.AppWindow is null)
        {
            progressDialogEx = new ProgressDialogEx();
            progressDialogEx.Closed += (s, e) => this.progressDialogEx = null;

        }
        progressDialogEx.Activate();
        progressDialogEx.CenterOnScreen();
    }

}
