using AppFirst.ViewModels.Dialogs;

namespace AppFirst.Views.Dialogs;

public partial class SqlConnectionStringDialog : ContentDialog
{
    public SqlConnectionStringDialogViewModel ViewModel { get; }

    public SqlConnectionStringDialog(string connectionString)
    {
        ViewModel = new SqlConnectionStringDialogViewModel(connectionString); //App.GetService<SqlConnectionStringDialogViewModel>();
        DataContext = ViewModel;
        this.InitializeComponent();
    }
}
