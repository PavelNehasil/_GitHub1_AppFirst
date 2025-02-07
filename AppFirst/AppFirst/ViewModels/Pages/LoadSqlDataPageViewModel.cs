using System.Collections.ObjectModel;
using AppFirst.Models;
using AppFirst.Services;
using AppFirst.Views.Dialogs;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;

namespace AppFirst.ViewModels.Pages;

public partial class LoadSqlDataPageViewModel : ObservableObject
{
    private readonly LoadSqlDataPageService _loadSqlDataPageService;

    [ObservableProperty]
    private bool _hasFailures = false;

    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private ObservableCollection<Order> _tableOrders = new ObservableCollection<Order>();

    [ObservableProperty]
    private string _connectionString = ((App)Application.Current).configurationJson.AppConnectionStringLocal;

    public LoadSqlDataPageViewModel()
    {
        _loadSqlDataPageService = new LoadSqlDataPageService(ConnectionString);
    }

    [RelayCommand]
    private async void OnShowSqlConnectionStringDialog()
    {
        var dialog = new SqlConnectionStringDialog(ConnectionString);
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.DefaultButton = ContentDialogButton.Primary;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            ((App)Application.Current).configurationJson.AppConnectionStringLocal = dialog.ViewModel.SqlConnectionString;
            ConnectionString = dialog.ViewModel.SqlConnectionString;
        }
    }

    [RelayCommand]
    public async Task OnReload()
    {
        IsLoading = true;
        HasFailures = false;
        TableOrders.Clear();

        try
        {
            await _loadSqlDataPageService.LoadSqlDataNowAsync(TableOrders);

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                //TableOrders.Clear();
                //foreach (var order in tableOrdersTmp)
                //{
                //    TableOrders.Add(order);
                //}
                //tableOrdersTmp.Clear();
                //ProgressDialogEx1.Close();
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            TableOrders.Clear();
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        await OnReload();
    }

    public void OnNavigatedFrom()
    {
    }
}
