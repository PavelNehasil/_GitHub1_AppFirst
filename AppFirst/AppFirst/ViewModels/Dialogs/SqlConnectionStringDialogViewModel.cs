using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;

namespace AppFirst.ViewModels.Dialogs;

internal class SqlServersMsqlGetFromRegistry
{
    public static void GetServers(ObservableCollection<string> destServers)
    {
        string ServerName = Environment.MachineName;
        RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
        using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
        {
            RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
            if (instanceKey != null)
            {
                foreach (var instanceName in instanceKey.GetValueNames())
                {
                    destServers.Add(ServerName + "\\" + instanceName);
                }
            }
        }
    }
}

public partial class SqlConnectionStringDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _dialogResult = false;

    [ObservableProperty]
    private string _sqlConnectionString = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> _serverNames = new ObservableCollection<string> { @".\", "localhost", "(LocalDB)\\MSSQLLocalDB", "(LocalDB)\\LocalDB" };

    [ObservableProperty]
    private string _selectedServerName;

    [ObservableProperty]
    private string _databaseName = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private bool _integratedSecurity = true;

    [ObservableProperty]
    private string _attachDBFilename = string.Empty;

    public SqlConnectionStringDialogViewModel(string connectionString)
    {
        SqlConnectionString = connectionString;
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        SqlServersMsqlGetFromRegistry.GetServers(ServerNames);
        ServerNames.Add(builder.DataSource);
        SelectedServerName = builder.DataSource;
        DatabaseName = builder.InitialCatalog;
        Username = builder.UserID;
        Password = builder.Password;
        IntegratedSecurity = builder.IntegratedSecurity;
        AttachDBFilename = builder.AttachDBFilename;
    }

    [RelayCommand]
    private void OnUpdateServername()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        try
        {
            builder.DataSource = SelectedServerName;
        }
        catch
        {
            SelectedServerName = @".\";
            builder.DataSource = SelectedServerName;
        }
        finally
        {
            SqlConnectionString = builder.ConnectionString;
        }
    }

    [RelayCommand]
    private void OnUpdateDatabaseName()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        builder.InitialCatalog = DatabaseName;
        SqlConnectionString = builder.ConnectionString;
    }

    [RelayCommand]
    private void OnUpdateUsername()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        builder.UserID = Username;
        SqlConnectionString = builder.ConnectionString;
    }

    [RelayCommand]
    private void OnUpdatePassword()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        builder.Password = Password;
        SqlConnectionString = builder.ConnectionString;
    }

    [RelayCommand]
    private void OnUpdateIntegratedSecurity()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        builder.IntegratedSecurity = IntegratedSecurity;
        SqlConnectionString = builder.ConnectionString;
    }

    [RelayCommand]
    private void OnUpdateAttachDBFilename()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(SqlConnectionString);
        builder.AttachDBFilename = AttachDBFilename;
        SqlConnectionString = builder.ConnectionString;
    }

    [RelayCommand]
    private void OnOkResult()
    {
        DialogResult = true;
        //CloseWindow();
    }

    [RelayCommand]
    private void OnCancelResult()
    {
        DialogResult = false;
        //CloseWindow();
    }
}
