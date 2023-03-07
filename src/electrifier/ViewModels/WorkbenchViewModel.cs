using CommunityToolkit.Mvvm.ComponentModel;

namespace electrifier.ViewModels;

public class WorkbenchViewModel : ObservableRecipient
{
    public bool IsBusy
    {
        get; set;
    }
    public string Title
    {
        get; set;
    }

    public WorkbenchViewModel()
    {
        Title = string.Empty;

    }
}
