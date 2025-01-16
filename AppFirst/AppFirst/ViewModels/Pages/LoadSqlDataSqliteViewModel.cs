using System.Collections.ObjectModel;
using System.Windows.Input;
using AppFirst.Classes;
using AppFirst.Models;
using AppFirst.Services;
using AppFirst.Views.Dialogs;
using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Windows.Storage.Pickers;
using WinUIEx;

namespace AppFirst.ViewModels.Pages;

public partial class LoadSqlDataSqliteViewModel : ObservableObject
{
    ImageDialogEx imageDialogEx = null;

    private readonly LoadSqlDataSqliteService _loadSqlDataSqliteService;
    private readonly LoadSqlDataSqliteService_LoginUser _loadSqlDataSqliteService_LoginUser;
    private readonly LoadSqlDataSqliteService_LoginType _loadSqlDataSqliteService_LoginType;
    public readonly LoadSqlDataSqliteService_LoginImage _loadSqlDataSqliteService_LoginImage;

    public string GetLoginType(int IdLoginType)
    {
        //var _getLoginType = TableLoginTypes.FirstOrDefault(x => x.Id == IdLoginType);
        //return _getLoginType?.Name ?? string.Empty;
        //return TableLoginTypes.FirstOrDefault(x => x.Id == IdLoginType).Name;
        return "Test";
    }

    //get{ return TableLoginTypes.FirstOrDefault(x => x.Id == IdLoginType).Name; }

    private User _selectedUser = null;

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            SetProperty(ref _selectedUser, value);
            _selectedUser = value;
            IsUserSelected = value is not null;
            OnReloadLoginUsers();
        }
    }

    [ObservableProperty]
    private LoginUser _selectedLoginUser = null;

    [ObservableProperty]
    private LoginImage _selectedLoginImage = null;

    [ObservableProperty]
    private bool _hasFailures = false;

    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private ObservableCollection<User> _tableUsers = new ObservableCollection<User>();

    [ObservableProperty]
    private ObservableCollection<User> _tableUsersAll = new ObservableCollection<User>();

    [ObservableProperty]
    private ObservableCollection<LoginUser> _tableLoginUsers = new ObservableCollection<LoginUser>();

    [ObservableProperty]
    private ObservableCollection<LoginType> _tableLoginTypes = new ObservableCollection<LoginType>();

    [ObservableProperty]
    private ObservableCollection<LoginImage> _tableLoginImages = new ObservableCollection<LoginImage>();

    [ObservableProperty]
    private string _connectionString = ((App)Application.Current).configurationJson.AppConnectionStringLocalSqlite1;

    public LoadSqlDataSqliteViewModel()
    {
        _loadSqlDataSqliteService = new LoadSqlDataSqliteService(ConnectionString);
        _loadSqlDataSqliteService_LoginUser = new LoadSqlDataSqliteService_LoginUser(ConnectionString);
        _loadSqlDataSqliteService_LoginType = new LoadSqlDataSqliteService_LoginType(ConnectionString);
        _loadSqlDataSqliteService_LoginImage = new LoadSqlDataSqliteService_LoginImage(ConnectionString);

        OnReloadLoginTypes();
        OnReloadLoginImages();
        //_searchCommand = new WinUICommunity.RelayCommand(OnSearchCommand);
    }

    [ObservableProperty]
    private bool _isUserSelected = false;

    private readonly ICommand _searchCommand;
    public ICommand SearchCommand => _searchCommand;
    public ICommand AddLoginImageCommand => new CommunityToolkit.Mvvm.Input.RelayCommand(AddLoginImageCommand_Executed);
    public ICommand DeleteLoginImageCommand => new CommunityToolkit.Mvvm.Input.RelayCommand<int>(DeleteLoginImageCommand_Executed);
    public ICommand DeleteUserCommand => new CommunityToolkit.Mvvm.Input.RelayCommand<int>(DeleteUserCommand_Executed);
    public ICommand EditUserCommand => new CommunityToolkit.Mvvm.Input.RelayCommand<int>(EditUserCommand_Executed);
    public ICommand DeleteLoginUserCommand => new CommunityToolkit.Mvvm.Input.RelayCommand<DateTime>(DeleteLoginUserCommand_Executed);
    public ICommand DeleteLoginUserIdCommand => new CommunityToolkit.Mvvm.Input.RelayCommand(DeleteLoginUserIdCommand_Executed);
    public ICommand CreateLoginEventCommand => new CommunityToolkit.Mvvm.Input.RelayCommand(CreateLoginEventCommand_Executed);
    public ICommand CreatePasswordEventCommand => new CommunityToolkit.Mvvm.Input.RelayCommand(CreatePasswordEventCommand_Executed);


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
            ((App)Application.Current).configurationJson.AppConnectionStringLocalSqlite1 = dialog.ViewModel.SqlConnectionString;
            ConnectionString = dialog.ViewModel.SqlConnectionString;
        }
    }

    [RelayCommand]
    public async Task OnReload()
    {
        IsLoading = true;
        HasFailures = false;
        var selectedUser = SelectedUser;
        TableUsers.Clear();
        TableUsersAll.Clear();

        try
        {
            await _loadSqlDataSqliteService.LoadSqlDataNowAsync(TableUsers);

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                TableUsersAll = new ObservableCollection<User>(TableUsers);
                if (selectedUser is not null && TableUsers.Contains(selectedUser))
                {
                    SelectedUser = selectedUser;
                }
                else if ((TableUsers.Count > 0) && (selectedUser is null))
                {
                    SelectedUser = TableUsers[0];
                }
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            TableUsers.Clear();
            TableUsersAll.Clear();
        }
    }

    [RelayCommand]
    public async Task OnReloadLoginUsers()
    {
        if (SelectedUser is null)
        {
            TableLoginUsers.Clear();
            return;
        }
        IsLoading = true;
        HasFailures = false;
        var selectedLoginUser = SelectedLoginUser;
        TableLoginUsers.Clear();

        try
        {
            await _loadSqlDataSqliteService_LoginUser.GetLoginUsers(TableLoginUsers, SelectedUser.Id);

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            TableLoginUsers.Clear();
        }
    }

    [RelayCommand]
    public async Task OnReloadLoginTypes()
    {
        IsLoading = true;
        HasFailures = false;
        TableLoginTypes.Clear();

        try
        {
            await _loadSqlDataSqliteService_LoginType.GetLoginTypes(TableLoginTypes);

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            TableLoginTypes.Clear();
        }
    }

    [RelayCommand]
    public async Task OnReloadLoginImages()
    {
        IsLoading = true;
        HasFailures = false;
        TableLoginImages.Clear();

        try
        {
            await _loadSqlDataSqliteService_LoginImage.GetLoginImages(TableLoginImages);

            DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

            await dispatcherQueue.EnqueueAsync(() =>
            {
                IsLoading = false;
            });
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
            TableLoginImages.Clear();
        }
    }

    [RelayCommand]
    public async void AddUserDialog()
    {
        var dialog = new UserDialog(TableUsers.Select(x => x.UserName).ToList());
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.ViewModel.TableLoginImages = TableLoginImages;
        dialog.ViewModel.loadSqlDataSqliteViewModel = this;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                await _loadSqlDataSqliteService.InsertUserAsync(dialog.ResultUser);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(async () =>
                {
                    await _loadSqlDataSqliteService_LoginUser.InsertLoginUserAsync(dialog.ResultUser.Id, 1, DateTime.Now);
                    IsLoading = false;
                    OnReload();
                    SelectedUser = dialog.ResultUser;

                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }

    [RelayCommand]
    public async void EditUserDialog()
    {
        if (SelectedUser is null)
        {
            return;
        }
        var userList = TableUsers.Select(x => x.UserName).ToList();
        userList.DeleteIfExists(SelectedUser.UserName);
        var dialog = new UserDialog(userList);
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.SetUser(SelectedUser);
        dialog.ViewModel.TableLoginImages = TableLoginImages;
        dialog.ViewModel.loadSqlDataSqliteViewModel = this;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                dialog.ResultUser.Id = SelectedUser.Id;
                await _loadSqlDataSqliteService.UpdateUserAsync(dialog.ResultUser);
                await _loadSqlDataSqliteService_LoginUser.InsertLoginUserAsync(dialog.ResultUser.Id, 2, DateTime.Now);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(() =>
                {
                    IsLoading = false;
                    OnReload();

                    SelectedUser = dialog.ResultUser;


                    //TableUsers.IndexOf(dialog.ResultUser);
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }

    [RelayCommand]
    public async void DeleteUserDialog()
    {
        if (SelectedUser is null)
        {
            return;
        }

        var dialog = new ContentDialog();
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Delete user?";
        dialog.Content = $"Do you want delete user({SelectedUser.UserName}) from database?";
        dialog.PrimaryButtonText = "Delete";
        dialog.CloseButtonText = "Cancel";
        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            if (SelectedUser is null)
            {
                return;
            }
            try
            {
                int index = TableUsers.IndexOf(SelectedUser);
                await _loadSqlDataSqliteService.DeleteUserAsync(SelectedUser);
                await _loadSqlDataSqliteService_LoginUser.DeleteLoginUserIdAsync(SelectedUser.Id);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(() =>
                {
                    IsLoading = false;
                    if (index > 0) --index;
                    TableUsers.Remove(SelectedUser);
                    OnReload();
                    if (TableUsers.Count > 0)
                    {
                        SelectedUser = TableUsers[index];
                    }
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }

    [RelayCommand]
    public async void DeleteLoginUserDialog()
    {
        if (SelectedLoginUser is null)
        {
            return;
        }

        var dialog = new ContentDialog();
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Delete login user?";
        dialog.Content = $"Do you want delete userId({(SelectedLoginUser.IdUser)}), type({(SelectedLoginUser.LoginType)}), date({(SelectedLoginUser.Date)}) from database?";
        dialog.PrimaryButtonText = "Delete";
        dialog.CloseButtonText = "Cancel";
        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                int index = TableLoginUsers.IndexOf(SelectedLoginUser);
                await _loadSqlDataSqliteService_LoginUser.DeleteLoginUserAsync(SelectedLoginUser);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(() =>
                {
                    IsLoading = false;
                    if (index > 0) --index;
                    TableLoginUsers.Remove(SelectedLoginUser);
                    OnReloadLoginUsers();
                    if (TableLoginUsers.Count > 0)
                    {
                        SelectedLoginUser = TableLoginUsers[index];
                    }
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }

    [RelayCommand]
    public async void DeleteLoginUserIdDialog()
    {
        if (SelectedUser is null)
        {
            return;
        }

        var dialog = new ContentDialog();
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Delete login user?";
        dialog.Content = $"Do you want delete userId({(SelectedUser.Id)}) events from database?";
        dialog.PrimaryButtonText = "Delete";
        dialog.CloseButtonText = "Cancel";
        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                await _loadSqlDataSqliteService_LoginUser.DeleteLoginUserIdAsync(SelectedUser.Id);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(() =>
                {
                    IsLoading = false;
                    OnReloadLoginUsers();
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }


    [RelayCommand]
    public async void DeleteLoginImageDialog()
    {
        if (SelectedLoginImage is null)
        {
            return;
        }

        var dialog = new ContentDialog();
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Delete login image?";
        dialog.Content = $"Do you want delete image Id({(SelectedLoginImage.Id)}) Name({SelectedLoginImage.ImageName}) from database?";
        dialog.PrimaryButtonText = "Delete";
        dialog.CloseButtonText = "Cancel";
        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                int index =  TableLoginImages.IndexOf(SelectedLoginImage);
                await _loadSqlDataSqliteService_LoginImage.DeleteLoginImageAsync(SelectedLoginImage.Id);

                DispatcherQueue dispatcherQueue = DispatcherQueue.GetForCurrentThread();

                await dispatcherQueue.EnqueueAsync(() =>
                {
                    if (index > 0) --index;
                    IsLoading = false;
                    OnReloadLoginImages();
                    if (TableLoginImages.Count > 0)
                    {
                        SelectedLoginImage = TableLoginImages[index];
                    }
                });

            }
            catch (Exception ex)
            {
                IsLoading = false;
                HasFailures = true;
            }
        }

    }

    [RelayCommand]
    public async void ViewLoginImageDialog()
    {
        if (SelectedLoginImage is null)
        {
            return;
        }

        if (imageDialogEx is null || imageDialogEx.AppWindow is null)
        {
            imageDialogEx = new ImageDialogEx();
            imageDialogEx.Closed += (s, e) => this.imageDialogEx = null;
        }
        imageDialogEx.SetImage(SelectedLoginImage.ImageName, SelectedLoginImage.ImageSource, SelectedLoginImage.ImageSource.PixelWidth, SelectedLoginImage.ImageSource.PixelHeight);

        imageDialogEx.Activate();
        imageDialogEx.CenterOnScreen();
    }

    [RelayCommand]
    public async void ViewLoginImageDialog2()
    {
        if (SelectedLoginImage is null)
        {
            return;
        }

        var dialog = new ImageDialog();
        dialog.XamlRoot = App.MainWindow.Content.XamlRoot;
        //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = SelectedLoginImage.ImageName;
        dialog.CloseButtonText = "Close";

        dialog.SetImage(SelectedLoginImage.ImageName, SelectedLoginImage.ImageSource, SelectedLoginImage.ImageSource.PixelWidth, SelectedLoginImage.ImageSource.PixelHeight);

        ContentDialogResult result = await dialog.ShowAsync();
    }

    [RelayCommand]
    public void OnMoveUpUser()
    {
        if ((SelectedUser is null) || (TableUsers.IndexOf(SelectedUser) == 0))
        {
            return;
        }

        User user = SelectedUser;
        TableUsers.Move(TableUsers.IndexOf(SelectedUser), TableUsers.IndexOf(SelectedUser) - 1);
        SelectedUser = user;
    }

    [RelayCommand]
    public void OnMoveDownUser()
    {
        if ((SelectedUser is null) || (TableUsers.IndexOf(SelectedUser) == TableUsers.Count - 1))
        {
            return;
        }
        User user = SelectedUser;
        TableUsers.Move(TableUsers.IndexOf(SelectedUser), TableUsers.IndexOf(SelectedUser) + 1);
        SelectedUser = user;
    }

    private void EditUserCommand_Executed(int id)
    {
        if (id == 0) return;
        SelectedUser = TableUsers.FirstOrDefault(x => x.Id == id);
        EditUserDialog();
    }

    private void DeleteUserCommand_Executed(int id)
    {
        if (id == 0) return;
        SelectedUser = TableUsers.FirstOrDefault(x => x.Id == id);
        DeleteUserDialog();
    }

    private void DeleteLoginUserIdCommand_Executed()
    {
        try
        {
            DeleteLoginUserIdDialog();
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
        }
    }

    private void DeleteLoginUserCommand_Executed(DateTime date)
    {
        if (date == default) return;
        try
        {
            SelectedLoginUser = TableLoginUsers.FirstOrDefault(x => x.Date == date);
            DeleteLoginUserDialog();

        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
        }
    }

    private void CreateLoginEventCommand_Executed()
    {
        try
        {
            if (SelectedUser is null)
            {
                return;
            }
            _loadSqlDataSqliteService_LoginUser.InsertLoginUserAsync(SelectedUser.Id, 3, DateTime.Now);
            OnReloadLoginUsers();
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
        }
    }

    private void CreatePasswordEventCommand_Executed()
    {
        try
        {
            if (SelectedUser is null)
            {
                return;
            }
            _loadSqlDataSqliteService_LoginUser.InsertLoginUserAsync(SelectedUser.Id, 4, DateTime.Now);
            OnReloadLoginUsers();
        }
        catch (Exception ex)
        {
            IsLoading = false;
            HasFailures = true;
        }
    }

    private async void AddLoginImageCommand_Executed()
    {
        var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

        var window = App.MainWindow;

        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        openPicker.FileTypeFilter.Add(".jpg");
        openPicker.FileTypeFilter.Add(".jpeg");
        openPicker.FileTypeFilter.Add(".png");
        openPicker.FileTypeFilter.Add(".bmp");

        var file = await openPicker.PickSingleFileAsync();
        if (file != null)
        {
            var loginImage = new LoginImage();
            loginImage.Image = ImageBlobConverter.ImageFilePathToBytes(file.Path);
            loginImage.ImageName = file.Name;
            loginImage.Description = file.Path;
            loginImage.ImageSource = ImageBlobConverter.ByteToBitmapAsync(loginImage.Image).Result;
            await _loadSqlDataSqliteService_LoginImage.InsertLoginImageAsync(loginImage);
            await OnReloadLoginImages();
            if (TableLoginImages.Count > 0)
                SelectedLoginImage = TableLoginImages[TableLoginImages.Count - 1];
        }
    }

    public void DeleteLoginImageCommand_Executed(int id)
    {
        if (id == 0) return;
        SelectedLoginImage = TableLoginImages.FirstOrDefault(x => x.Id == id);
        DeleteLoginImageDialog();
    }

}
