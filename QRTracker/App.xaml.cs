using NewRelic.MAUI.Plugin;
using QRTracker.Constants;
using QRTracker.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace QRTracker;

[RequiresUnreferencedCode("Calls SetInvokeJavaScriptTarget.")]
public partial class App : Application
{
    private MainApplicationWindowViewModel _MainApplicationViewModel;
    public App(MainApplicationWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        _MainApplicationViewModel = mainWindowViewModel;    
        RegisterRoutes();
        //InitializeNewRelic();
    }

    public void RegisterRoutes()
    {
        Routing.RegisterRoute(Routes.QRItemDetailRoute, typeof(QRItemDetailPage));
        Routing.RegisterRoute(Routes.AboutRoute, typeof(AboutPage));
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new MainAppWindow(_MainApplicationViewModel);
    }

    protected override void OnStart()
    {
        base.OnStart();
#if ANDROID || IOS
        CrossNewRelic.Current.HandleUncaughtException();
        CrossNewRelic.Current.TrackShellNavigatedEvents();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            CrossNewRelic.Current.Start("AA52d55d7ccd625cd835f754b73458b6b724169c5d-NRMA");
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            CrossNewRelic.Current.Start("AA016ea8f9365e5dbb11908aee885d8110e407ef86-NRMA");
        }
#endif
    }
}