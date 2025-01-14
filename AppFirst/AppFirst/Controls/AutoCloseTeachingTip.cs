namespace AppFirst.Controls;

public class AutoCloseTeachingTip : Microsoft.UI.Xaml.Controls.TeachingTip
{
    private DispatcherTimer _timer;
    private long _token;

    public AutoCloseTeachingTip() : base()
    {
        this.Loaded += AutoCloseTeachingTip_Loaded;
        this.Unloaded += AutoCloseTeachingTip_Unloaded;
    }

    public int AutoCloseInterval { get; set; } = 3000;

    private void AutoCloseTeachingTip_Loaded(object sender, RoutedEventArgs e)
    {
        _token = this.RegisterPropertyChangedCallback(IsOpenProperty, IsOpenChanged);
        if (IsOpen)
        {
            Open();
        }
    }

    private void AutoCloseTeachingTip_Unloaded(object sender, RoutedEventArgs e)
    {
        this.UnregisterPropertyChangedCallback(IsOpenProperty, _token);
    }

    private void IsOpenChanged(DependencyObject o, DependencyProperty p)
    {
        var that = o as AutoCloseTeachingTip;
        if (that == null)
        {
            return;
        }

        if (p != IsOpenProperty)
        {
            return;
        }

        if (that.IsOpen)
        {
            that.Open();
        }
        else
        {
            that.Close();
        }
    }

    private void Open()
    {
        _timer = new DispatcherTimer();
        _timer.Tick += Timer_Tick;
        _timer.Interval = TimeSpan.FromMilliseconds(AutoCloseInterval);
        _timer.Start();
    }

    private void Close()
    {
        if (_timer == null)
        {
            return;
        }

        _timer.Stop();
        _timer.Tick -= Timer_Tick;
    }

    private void Timer_Tick(object sender, object e)
    {
        this.IsOpen = false;
    }
}
