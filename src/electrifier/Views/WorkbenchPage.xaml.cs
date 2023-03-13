using electrifier.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.CodeDom;
using System.Diagnostics;
using System.Text;

namespace electrifier.Views;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public sealed partial class WorkbenchPage : Page
{
    public bool WarrantyDisclaimerInfoBarInfoBarIsOpen
    {
        get;
        set;
    }

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

        WarrantyDisclaimerInfoBarInfoBarIsOpen = true;
    }

    private string GetDebuggerDisplay()
    {
        return new StringBuilder("WorkBenchPage.\n")
            .AppendLine($"ViewModel: { ViewModel.ToString() }")
            .ToString();
    }
}