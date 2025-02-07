using AppFirst.Services;
using AppFirst.ViewModels.Pages;

namespace AppFirst.Views.Pages;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class LoadSqlDataGridPage : Page
{
    public LoadSqlDataGridViewModel ViewModel
    {
        get;
    }

    public LoadSqlDataGridPage()
    {
        ViewModel = new LoadSqlDataGridViewModel(new LoadSqlDataGridService(((App)Application.Current).configurationJson.AppConnectionStringLocal));
        //App.GetService<LoadSqlDataGridViewModel>();
        DataContext = ViewModel;
        InitializeComponent();

        //Task.Run(async () => ViewModel.OnReload());
        ViewModel.OnReload();
    }
}
