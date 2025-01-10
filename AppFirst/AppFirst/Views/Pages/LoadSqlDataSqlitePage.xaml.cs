using AppFirst.Models;
using AppFirst.ViewModels.Pages;
using Microsoft.UI.Xaml.Input;

namespace AppFirst.Views.Pages;

public sealed partial class LoadSqlDataSqlitePage : Page
{
    public LoadSqlDataSqliteViewModel ViewModel { get; } = new();


    public LoadSqlDataSqlitePage()
    {
        //ViewModel = App.GetService<LoadSqlDataSqliteViewModel>();
        DataContext = ViewModel;
        InitializeComponent();

        //Task.Run(async () => ViewModel.OnReload());
        ViewModel.OnReload();
    }

    public void OnSearchCommand(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        ViewModel.TableUsers.Clear();
        string searchText = sender.Text.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            ViewModel.TableUsers.AddRange(ViewModel.TableUsersAll);
        }
        else
        {
            ViewModel.TableUsers.AddRange(ViewModel.TableUsersAll.Where(x => x.UserName.ToLower().Contains(searchText)));
        }

        //ViewModel.TableUsers = new ObservableCollection<User>(ViewModel.TableUsersAll.Where(x => x.UserName.ToLower().Contains(searchText)));

        if (ViewModel.TableUsers.Count > 0)
        {
            ViewModel.SelectedUser = ViewModel.TableUsers[0];
        }
    }

    private void ListViewSwipeContainer_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == Microsoft.UI.Input.PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == Microsoft.UI.Input.PointerDeviceType.Pen)
        {
            VisualStateManager.GoToState(sender as Control, "HoverButtonsShown", true);
        }
    }

    private void ListViewSwipeContainer_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        VisualStateManager.GoToState(sender as Control, "HoverButtonsHidden", true);
    }


    private void MenuFlyoutItemLoginImage_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is MenuFlyoutItem menuFlyoutItem)
        {
            //if (menuFlyoutItem.DataContext is LoginImage loginImage)
            if (ViewModel.SelectedLoginImage != null)
            {
                ViewModel.DeleteLoginImageCommand_Executed(ViewModel.SelectedLoginImage.Id);
            }
        }
    }

    private void ListViewLoginImage_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
        var view = (ListView)sender;
        ViewModel.SelectedLoginImage = ((FrameworkElement)e.OriginalSource).DataContext as LoginImage;

        var viewPoint = e.GetPosition(view);

        ContextMenuLoginImage.ShowAt(view, viewPoint);
    }

    private void ListViewLoginImage_Holding(object sender, HoldingRoutedEventArgs e)
    {
        var view = (ListView)sender;
        ViewModel.SelectedLoginImage = ((FrameworkElement)e.OriginalSource).DataContext as LoginImage;

        var viewPoint = e.GetPosition(view);

        ContextMenuLoginImage.ShowAt(view, viewPoint);
    }
}


