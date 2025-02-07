// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AppFirst.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Sample1Page : Page
{
    public Sample1PageViewModel ViewModel { get; }
    public Sample1Page()
    {
        ViewModel = App.GetService<Sample1PageViewModel>();
        this.InitializeComponent();
    }



}
