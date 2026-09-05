using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation.Regions;
using SampleApp.Services;
using SampleApp.ViewModels;
using SampleApp.Views;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace SampleApp;

public partial class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // Required when overriding Initialize
        base.Initialize();

#if DEBUG
        // Replaces the old this.AttachDevTools();
        // NOTE: This requires connection to, http://127.0.0.1:29414/ and some IT firewalls may block it.
        // Reference: https://docs.avaloniaui.net/tools/developer-tools/attaching-to-the-remote-tool
        this.AttachDeveloperTools();
        ////{
        ////    // Change the initialization key gesture (Default is F12)
        ////    options.Gesture = Avalonia.Input.KeyGesture.Parse("F11");
        ////});
#endif
    }

    protected override AvaloniaObject CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Services
        containerRegistry.RegisterSingleton<INotificationService, NotificationService>();

        // Views - Dialogs
        containerRegistry.RegisterDialog<MessageBoxView, MessageBoxViewModel>();
        ////containerRegistry.RegisterDialogWindow<CustomDialogWindow>(nameof(CustomDialogWindow));

        // Views - Region Navigation
        containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
        containerRegistry.RegisterForNavigation<DialogSampleView, DialogSampleViewModel>();
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<SubSettingsView, SubSettingsViewModel>();
    }

    /// <summary>Called after Initialize.</summary>
    protected override void OnInitialized()
    {
        // Register Views to the Region it will appear in. Don't register them in the ViewModel.
        var regionManager = Container.Resolve<IRegionManager>();

        // WARNING: Prism v11.0.0
        // - DataTemplates MUST define a DataType or else an XAML error will be thrown
        // - Error: DataTemplate inside of DataTemplates must have a DataType set
        regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DashboardView));
    }
}
