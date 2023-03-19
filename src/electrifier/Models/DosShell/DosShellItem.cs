using muxc = Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI;
using Microsoft.UI.Xaml;

namespace electrifier.Services;

public class DosShellItem : INotifyPropertyChanged, IEquatable<DosShellItem?>
{

    public string FileName => (StorageItem != null ? StorageItem?.Name : "[Not Initialized]");



    public string FileType => (StorageItem != null ? StorageItem?.Name : "[Not Initialized]");


    public override int GetHashCode()
    {
        return StorageItem.GetHashCode();
    }

    public bool IsFolder
    {
        get; private set;
    }

    public bool IsFile => !IsFolder;

    public IconId IconId
    {
        get; private set;
    }
    public ImageIcon ShellIcon
    {
        get; private set;
    }

    public IStorageItem StorageItem
    {
        get; private set;
    }

    public DosShellItem()
    {
        IconId = IconId = new IconId((ulong)(IsFolder ? 0x0 : 0x1));
    }

    public DosShellItem(IStorageItem storageItem)
    {
        StorageItem = storageItem;
        IsFolder = storageItem.IsOfType(StorageItemTypes.Folder);
        IconId = new IconId((ulong)(IsFolder ? 0x0 : 0x1));
        ShellIcon = new ImageIcon();
    }


    private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        // TODO: LogMessage

        var image = new Image();
        image.ImageFailed += Image_ImageFailed;

        var imageIcon = new ImageIcon();
        ShellIcon = imageIcon;

        OnPropertyChanged(nameof(IconId));
        OnPropertyChanged(nameof(ShellIcon));
    }

    #region events

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion events

    #region IEquatable<DosShellItem?>

    public override bool Equals(object? obj) => Equals(obj as DosShellItem);
    public bool Equals(DosShellItem? other) => other is not null &&
        EqualityComparer<IStorageItem>.Default.Equals(StorageItem, other.StorageItem) &&
        EqualityComparer<IStorageItem>.Default.Equals(StorageItem, other.StorageItem);

    public static bool operator ==(DosShellItem? left, DosShellItem? right) => EqualityComparer<DosShellItem>.Default.Equals(left, right);

    public static bool operator !=(DosShellItem? left, DosShellItem? right) => !(left == right);

    #endregion IEquatable<DosShellItem?>
}
