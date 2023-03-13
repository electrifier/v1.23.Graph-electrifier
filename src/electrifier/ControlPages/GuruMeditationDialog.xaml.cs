using System.Diagnostics;
using System.Text;
using Microsoft.UI.Xaml.Controls;

namespace electrifier.ControlPages;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public sealed partial class GuruMeditationDialoge : ContentDialog
{
    /*
    public GuruMeditationViewModel ViewModel
    {
        get;
    }
    */


    public GuruMeditationDialoge()
    {
        // ViewModel = App.GetService<GuruMeditationViewModel>();
        //InitializeComponent();

        //var loggerInfo = new LoggerFactory().CreateLogger("Guru Medition");

//        var logger = Logger.GetLogger(loggerInfo);

        // LoggerInformation[]

    }

    internal string GetDebuggerDisplay()
    {
        try
        {
            var msg = new StringBuilder();

            msg.AppendLine($"Current Position: GuruMeditationDialoge.GetDebuggerDisplay\n");
            msg.AppendLine($"\n");
            msg.AppendLine(this.ToString());

            return msg.ToString();
        }
        catch (Exception ex)
        {
            return $"Fatal error: GuruMeditationDialoge.GetDebuggerDisplay() failed:\n{ex.ToString()}";
        }
    }

    public GuruMeditationDialoge(Button showDialog, TextBlock dialogResult, bool contentLoaded)
    {
        this.ShowDialog_Click(this, null);

        //ShowDialog = showDialog ?? throw new ArgumentNullException(nameof(showDialog));
        //DialogResult = dialogResult ?? throw new ArgumentNullException(nameof(dialogResult));
        //_contentLoaded = contentLoaded;
    }



    public async void ThrowGuruMeditation(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs args) => ShowDialog_Click(sender, args);


    private async void ShowDialog_Click(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs? args)
    {
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine($"*** GURU MEDITATION ***\n");

            if(args != null)
            {
                sb.AppendLine($"Reason: {args.ToString()} \n");
            }

            await ShowAsync();
        }
        catch (Exception)
        {
            // TODO: log exception
        }
        finally
        {
            // TODO: Clean up, save config.
        }


        //ContentDialogExample dialog = new ContentDialogExample();

        //// XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        //dialog.XamlRoot = this.XamlRoot;
        //dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        //dialog.Title = "Save your work?";
        //dialog.PrimaryButtonText = "Save";
        //dialog.SecondaryButtonText = "Don't Save";
        //dialog.CloseButtonText = "Cancel";
        //dialog.DefaultButton = ContentDialogButton.Primary;
        //dialog.Content = new ContentDialogContent();

        //var result = await dialog.ShowAsync();

        //if (result == ContentDialogResult.Primary)
        //{
        //    DialogResult.Text = "User saved their work";
        //}
        //else if (result == ContentDialogResult.Secondary)
        //{
        //    DialogResult.Text = "User did not save their work";
        //}
        //else
        //{
        //    DialogResult.Text = "User cancelled the dialog";
        //}
    }
}