using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

public sealed partial class WorkbenchPage : Page
{
    public WorkbenchViewModel ViewModel
    {
        get;
    }
#if DEBUG
    private InfoBarSeverity InfoBarSeverity => InfoBarSeverity.Warning;
#else
    private InfoBarSeverity InfoBarSeverity => InfoBarSeverity.Informational;
#endif

    public WorkbenchPage()
    {
        ViewModel = App.GetService<WorkbenchViewModel>();
        InitializeComponent();
    }
}
