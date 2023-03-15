using System.Reflection.Metadata;
using electrifier.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.Text;
using Windows.ApplicationModel.Resources;
using Microsoft.UI.Xaml;
using Microsoft.Identity.Client;
using CommunityToolkit.Authentication;
using Microsoft.Graph;
using Azure.Identity;

namespace electrifier.Views;

public sealed partial class WorkbenchPage : Page
{
    public GraphServiceClient GraphServiceClient
    {
        get;
    }
    public WorkbenchViewModel ViewModel
    {
        get;
    }
    public bool WarrantyDisclaimerInfoBarInfoBarIsOpen
    {
        get;
        set;
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
    private void ButtonTajbenderHotmail_OnClickAsync(object sender, RoutedEventArgs args)
    {
        try
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common", single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = "common";

            // Value from app registration
            var clientId = "67501219-8281-4e6f-8348-aad08088c13b";

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var userName = "tajbender@hotmail.de";
            var password = string.Empty;

            // https://learn.microsoft.com/dotnet/api/azure.identity.usernamepasswordcredential
            var userNamePasswordCredential = new UsernamePasswordCredential(
                userName,
                password,
                tenantId,
                clientId,
                options);


        //        var result = graphClient.Me.Messages["{message-id}"].GetAsync();
        //        var result2 = graphClient.Me.Messages["{message-id}"].SingleValueExtendedProperties;
            var graphClient = new GraphServiceClient(userNamePasswordCredential, scopes);

            //        var msalClient = PublicClientApplicationBuilder.Create(appId)
            //            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            //            .Build();

            //        ProviderManager.Instance.GlobalProvider = new MsalProvider(msalClient, scopes.Split(' '));

            //        // Handle auth state change
            //        ProviderManager.Instance.ProviderStateChanged += GraphProviderUpdated;
        }
        catch (Exception ex)
        {
            var message = ex.ToString();
            throw;
        }

        //        var result = graphClient.Me.Messages["{message-id}"].GetAsync();
        //        var result2 = graphClient.Me.Messages["{message-id}"].SingleValueExtendedProperties;
        //var strings = result?.BodyPreview;
        //try
        //{
        //    var graphClient = new GraphServiceClient(requestAdapter);
        //    var result = await graphClient.Me.Messages["{message-id}"].GetAsync();
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}
        //// Load OAuth settings
        //try
        //{
        //    //var oauthSettings = ResourceLoader.GetForCurrentView("OAuth");
        //    //var appId = oauthSettings.GetString("AppId");
        //    //var scopes = oauthSettings.GetString("Scopes");
        //    var appId = "";
        //    var scopes = "";
        //    if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(scopes))
        //    {
        //        // TODO:            Notification.Show("Could not load OAuth Settings from resource file.");
        //    }
        //    else
        //    {
        //        // Configure MSAL provider
        //        var msalClient = PublicClientApplicationBuilder.Create(appId)
        //            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
        //            .Build();
        //        ProviderManager.Instance.GlobalProvider = new MsalProvider(msalClient, scopes.Split(' '));
        //        // Handle auth state change
        //        ProviderManager.Instance.ProviderStateChanged += GraphProviderUpdated;
        //        //// Navigate to HomePage.xaml
        //        //RootFrame.Navigate(typeof(HomePage));
        //    }
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}


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