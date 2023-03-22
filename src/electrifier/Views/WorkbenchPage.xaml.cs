using electrifier.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using CommunityToolkit.Authentication;
using Microsoft.Graph;
using Azure.Identity;


namespace electrifier.Views;

/// <summary>
/// <see cref="WorkbenchPage"/> is the Home aka Start page.
/// Users can select, create and manage their workbenches here.
///         https://learn.microsoft.com/en-us/windows/apps/design/controls/command-bar
/// </summary>
public sealed partial class WorkbenchPage : Page
{
    public GraphServiceClient? GraphServiceClient
    {
        get;
        private set;
    }

    public WorkbenchViewModel ViewModel
    {
        get;
    }

    public bool WarrantyDisclaimerInfoBarInfoBarIsOpen
    {
        get;
        set;
    } = false;

    public WorkbenchPage()
    {
        ViewModel = App.GetService<WorkbenchViewModel>() ??
                    throw new InvalidOperationException(nameof(ViewModel));

        InitializeComponent();
        WarrantyDisclaimerInfoBarInfoBarIsOpen = false;

        ConnectToGraph();
    }

    public void ConnectToGraph()
    {
        try
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "common";

            // Value from app registration
            var clientId = "YOUR_CLIENT_ID";

            // using Azure.Identity;
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // Callback function that receives the user prompt
            // Prompt contains the generated device code that you must
            // enter during the auth process in the browser
            Func<DeviceCodeInfo, CancellationToken, Task> callback = (code, cancellation) =>
            {
                Console.WriteLine(code.Message);
                return Task.FromResult(0);
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.devicecodecredential
            var deviceCodeCredential = new DeviceCodeCredential(
                callback, tenantId, clientId, options);

            var graphClient = new GraphServiceClient(deviceCodeCredential, scopes);
        }
        catch (Exception ex)
        {
            var message = ex.ToString();
            throw;
        }
    }

    private void GraphProviderUpdated(object? sender, ProviderStateChangedEventArgs e)
    {
    }

    private void ButtonTajElectrifier_OnClick(object sender, RoutedEventArgs e)
    {
    }
    private void ButtonMSFTDummy_OnClick(object sender, RoutedEventArgs e)
    {
    }
}