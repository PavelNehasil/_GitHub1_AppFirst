using AppFirst.Classes.Config;
using AppFirst.ViewModels.Dialogs;
using AppFirst.ViewModels.Pages;
using AppFirst.Views.Pages;
using WinUIEx;

namespace AppFirst;

public partial class App : Application
{
    public static Window MainWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public IJsonNavigationViewService GetJsonNavigationViewService => GetService<IJsonNavigationViewService>();
    public IThemeService GetThemeService => GetService<IThemeService>();

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)!.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public App()
    {
        Services = ConfigureServices();
        this.InitializeComponent();
        configurationJson = new JsonFileConfiguration();
        configurationJson.Initialize();



    }



    public JsonFileConfiguration configurationJson { get; set; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IJsonNavigationViewService>(factory =>
        {
            var json = new JsonNavigationViewService();
            json.ConfigDefaultPage(typeof(Data1Page));
            json.ConfigDefaultPage(typeof(Data2Page));
            json.ConfigDefaultPage(typeof(Data3Page));
            json.ConfigSettingsPage(typeof(SettingsPage));
            json.ConfigDefaultPage(typeof(Sample1Page));
            json.ConfigDefaultPage(typeof(SamplePage));
            json.ConfigDefaultPage(typeof(LoadSqlDataPage));
            json.ConfigDefaultPage(typeof(LoadSqlDataGridPage));
            json.ConfigDefaultPage(typeof(LoadSqlDataSqlitePage));
            json.ConfigDefaultPage(typeof(HomeLandingPage));


            return json;
        });

        services.AddTransient<MainViewModel>();
        services.AddTransient<GeneralSettingViewModel>();
        services.AddTransient<AppUpdateSettingViewModel>();
        services.AddTransient<AboutUsSettingViewModel>();
        services.AddTransient<Sample1PageViewModel>();
        services.AddTransient<SamplePageViewModel>();
        services.AddTransient<LoginDialogViewModel>();
        services.AddTransient<UserDialogViewModel>();
        services.AddTransient<LoadSqlDataPageViewModel>();
        services.AddTransient<LoadSqlDataSqliteViewModel>();
        services.AddTransient<SqlConnectionStringDialogViewModel>();
        services.AddTransient<LoadSqlDataGridViewModel>();


        return services.BuildServiceProvider();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new WindowEx();

        if (MainWindow.Content is not Frame rootFrame)
        {
            MainWindow.Content = rootFrame = new Frame();
        }

        if (GetThemeService != null)
        {
            GetThemeService.AutoInitialize(MainWindow);
        }

        rootFrame.Navigate(typeof(MainPage));

        MainWindow.Title = MainWindow.AppWindow.Title = ProcessInfoHelper.ProductNameAndVersion;
        MainWindow.AppWindow.SetIcon("Assets/icon.ico");

        var splash = new Views.SplashScreen(MainWindow);
        splash.Completed += (s, e) => MainWindow = (WindowEx)e;


        //MainWindow.Activate();


    }


}

