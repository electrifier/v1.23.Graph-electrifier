using electrifier.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Text;

namespace electrifier.Views;

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

    public WorkbenchPage()
    {
        ViewModel = App.GetService<WorkbenchViewModel>() ??
                    throw new InvalidOperationException(nameof(ViewModel));

        InitializeComponent();

        // TODO: Read and write settings
        WarrantyDisclaimerInfoBarInfoBarIsOpen = false;


        //var graphClient = new GraphServiceClient(requestAdapter);
        //var manager = await graphClient.Me.Manager.GetAsync();

    }
}