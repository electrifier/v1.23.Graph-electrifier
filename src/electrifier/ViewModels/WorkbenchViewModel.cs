using CommunityToolkit.Mvvm.ComponentModel;

namespace electrifier.ViewModels;

public class WorkbenchViewModel : ObservableRecipient
{

    public string Title
    {
        get; set;
    }

    public WorkbenchViewModel()
    {
        Title = "Workbench";
    }
}