using System.Collections.ObjectModel;
using AppFirst.Contracts.Services;
using AppFirst.Models;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;

namespace AppFirst.ViewModels.Pages;

public partial class LoadSqlDataGridViewModel : ObservableRecipient
{
    private readonly ILoadSqlDataGridService loadSqlDataGridService;

    public ObservableCollection<Order> Source { get; } = new ObservableCollection<Order>();

    [ObservableProperty]
    private bool _hasFailures = false;

    [ObservableProperty]
    private bool _isLoading = true;

    public LoadSqlDataGridViewModel(ILoadSqlDataGridService loadSqlDataGridService)
    {
        this.loadSqlDataGridService = loadSqlDataGridService;
    }

    [RelayCommand]
    public async Task OnReload()
    {
        IsLoading = true;
        HasFailures = false;
        Source.Clear();

        try
        {
            var data = await this.loadSqlDataGridService.GetGridDataAsync();

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                Source.Clear();
                foreach (var item in data)
                {
                    Source.Add(item);
                }
                //ProgressDialogEx1.Close();
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            Source.Clear();
        }
    }
}
