using electrifier.Services;
using System.Collections.ObjectModel;
using electrifier.ViewModels;

using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;

namespace electrifier.Views;

/// <summary>
/// FileManagerPage
/// 
/// as taken from <see href="https://learn.microsoft.com/en-us/windows/apps/get-started/simple-photo-viewer-winui3"/>
/// </summary>
public sealed partial class FileManagerPage : Page
{
    public ObservableCollection<ImageFileInfo> Images { get; } = new ObservableCollection<ImageFileInfo>();

    public FileManagerViewModel ViewModel
    {
        get;
    }

    /// <summary>
    /// FileManagerPage
    /// 
    /// as taken from <see href="https://learn.microsoft.com/en-us/windows/apps/get-started/simple-photo-viewer-winui3"/>
    /// </summary>
    public FileManagerPage()
    {
        ViewModel = App.GetService<FileManagerViewModel>();
        InitializeComponent();

        _ = GetItemsAsync();
    }

    private void ImageGridView_ContainerContentChanging(
        ListViewBase sender,
        ContainerContentChangingEventArgs args)
    {
        if (args.InRecycleQueue)
        {
            var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
            var image = templateRoot?.FindName("ItemImage") as Image;
            image.Source = null;
        }

        if (args.Phase == 0)
        {
            args.RegisterUpdateCallback(ShowImage);
            args.Handled = true;
        }
    }

    private async void ShowImage(
        ListViewBase sender, 
        ContainerContentChangingEventArgs args)
    {
        if (args.Phase == 1)
        {
            // It's phase 1, so show this item's image.
            var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
            var image = templateRoot.FindName("ItemImage") as Image;
            var item = args.Item as ImageFileInfo;
            image.Source = await item.GetImageThumbnailAsync();
        }
    }

    /// <summary>
    /// GetItemsAsync
    /// 
    /// as taken from <see href="https://learn.microsoft.com/en-us/windows/apps/get-started/simple-photo-viewer-winui3"/>
    /// </summary>
    private async Task GetItemsAsync()
    {
        StorageFolder picturesFolder;

        //picturesFolder = Package.Current.InstalledLocation;
        //picturesFolder = KnownFolders.PicturesLibrary;
        picturesFolder = KnownFolders.DocumentsLibrary;
        //picturesFolder = KnownFolders.HomeGroup;

        var result = picturesFolder.CreateFileQueryWithOptions(new QueryOptions());

        IReadOnlyList<StorageFile> imageFiles = await result.GetFilesAsync();
        foreach (StorageFile file in imageFiles)
        {
            Images.Add(await LoadImageInfo(file));
        }

        ImageGridView.ItemsSource = Images;
    }

    /// <summary>
    /// LoadImageInfo
    /// 
    /// as taken from <see href="https://learn.microsoft.com/en-us/windows/apps/get-started/simple-photo-viewer-winui3"/>
    /// </summary>
    public static async Task<ImageFileInfo> LoadImageInfo(StorageFile file)
    {
        var properties = await file.Properties.GetImagePropertiesAsync();

        ImageFileInfo info = new(properties,
                                 file, 
                                 file.DisplayName, 
                                 file.DisplayType);

        return info;
    }
}
