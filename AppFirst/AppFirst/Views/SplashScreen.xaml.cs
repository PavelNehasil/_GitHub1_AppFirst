﻿// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AppFirst.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreen : WinUIEx.SplashScreen
    {
        public SplashScreen(Window window) : base(window)
        {
            this.InitializeComponent();
            this.SystemBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
        }

        protected override async Task OnLoading()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                this.IsAlwaysOnTop = false;
            for (int i = 0; i < 100; i += 5)
            {
                status.Text = $"Loading {i}%...";
                progress.Value = i;
                await Task.Delay(50);
            }
        }
    }
}
