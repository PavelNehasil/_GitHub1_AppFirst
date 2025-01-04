// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using AppFirst.ViewModels.Pages;

namespace AppFirst.Views.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SamplePage : Page
{
    public SamplePageViewModel ViewModel { get; }
    public SamplePage()
    {
        ViewModel = App.GetService<SamplePageViewModel>();
        DataContext = ViewModel;
        this.InitializeComponent();
    }

    private async void ShowLoginError()
    {
        var messageDialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Title = "Login Error",
            Content = "Invalid username or password",
            CloseButtonText = "OK"
        };

        await messageDialog.ShowAsync();
    }

    public async void ShowLoginDialogClick(object sender, RoutedEventArgs e)
    {


    }
}
