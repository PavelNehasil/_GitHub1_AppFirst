using AppFirst.ViewModels.Pages;

namespace AppFirst.Views.Pages;

public sealed partial class LoadSqlDataPage : Page
{
    public LoadSqlDataPageViewModel ViewModel { get; }


    public LoadSqlDataPage()
    {
        ViewModel = App.GetService<LoadSqlDataPageViewModel>();
        DataContext = ViewModel;
        InitializeComponent();

        //Task.Run(async () => ViewModel.OnReload());
        ViewModel.OnReload();
    }
}


