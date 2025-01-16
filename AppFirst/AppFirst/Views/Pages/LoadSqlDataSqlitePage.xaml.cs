using AppFirst.Models;
using AppFirst.ViewModels.Pages;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

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
            var tag = (sender as MenuFlyoutItem)!.Tag;
            switch (tag)
            {
                case "delete":
                    {
                        if (ViewModel.SelectedLoginImage == null)
                            break;

                        ViewModel.DeleteLoginImageCommand_Executed(ViewModel.SelectedLoginImage.Id);
                        break;
                    }
                case "view":
                    {
                        if (ViewModel.SelectedLoginImage == null)
                            break;

                        ViewModel.ViewLoginImageDialog();
                        break;
                    }
                case "view2":
                    {
                        if (ViewModel.SelectedLoginImage == null)
                            break;

                        ViewModel.ViewLoginImageDialog2();
                        break;
                    }

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

    private void TestButton3Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        ViewModel.SelectedLoginImage = button.DataContext as LoginImage;

        var listViewItem = FindParent<ListViewItem>(button);

        if (listViewItem != null)
        {
            var teachingTip = FindDescendant<TeachingTip>(listViewItem, t => t.Name == "ToggleThemeTeachingTip3");

            if (teachingTip != null)
            {
                teachingTip.IsOpen = true;
            }
        }
    }

    // Helper method to find a parent of a specific type
    private T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject == null) return null;

        T parent = parentObject as T;
        if (parent != null)
        {
            return parent;
        }
        else
        {
            return FindParent<T>(parentObject);
        }
    }

    // Helper method to find a descendant of a specific type and condition
    private T FindDescendant<T>(DependencyObject element, Func<T, bool> condition) where T : FrameworkElement
    {
        if (element == null)
            return null;

        int childCount = VisualTreeHelper.GetChildrenCount(element);
        for (int i = 0; i < childCount; i++)
        {
            var child = VisualTreeHelper.GetChild(element, i);
            if (child is T frameworkElement && condition(frameworkElement))
            {
                return frameworkElement;
            }

            var descendant = FindDescendant(child, condition);
            if (descendant != null)
            {
                return descendant;
            }
        }

        return null;
    }

    private void ListViewLoginImage_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        ViewModel.ViewLoginImageDialog();
    }
}


