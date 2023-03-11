using System.CodeDom;
using System.Diagnostics;
using System.Text;
using electrifier.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace electrifier.Views;

//[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public sealed partial class WorkbenchPage : Page
{
#if DEBUG
    public InfoBarSeverity InfoBarSeverity => InfoBarSeverity.Warning;
#else
    public InfoBarSeverity InfoBarSeverity => InfoBarSeverity.Success;
#endif

    /// <summary>
    /// 
    /// </summary>
    public WorkbenchViewModel ViewModel
    {
        get;
    }

    /// <summary>
    /// 
    /// </summary>
    public WorkbenchPage()
    {
        ViewModel = App.GetService<WorkbenchViewModel>() ?? 
                    throw new InvalidOperationException(nameof(ViewModel));

        InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private string GetDebuggerDisplay()
    {
        return new StringBuilder("WorkBenchPage.\n")
            .AppendLine($"ViewModel: { ViewModel.ToString() }")
            .ToString();
    }
}