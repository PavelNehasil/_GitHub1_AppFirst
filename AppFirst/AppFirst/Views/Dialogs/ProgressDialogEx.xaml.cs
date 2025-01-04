using WinUIEx;

namespace AppFirst.Views.Dialogs;

public sealed partial class ProgressDialogEx : WindowEx
{
    public ProgressDialogEx()
    {
        InitializeComponent();

    }

    private void OnClose(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
