﻿using muxc = Microsoft.UI.Xaml.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI;

namespace electrifier.Services;

public class DosShellItem : INotifyPropertyChanged, IEquatable<DosShellItem?>
{

    public string FileName => StorageItem.Name;

    public string FileType => StorageItem.Name;

    protected bool isFolder;

    public override int GetHashCode()
    {
        return StorageItem.GetHashCode();
    }

    public bool IsFolder => isFolder;

    public bool IsFile => !IsFolder;

    public IconId IconId
    {
        get; private set;
    }
    public ImageIcon ShellIcon
    {
        get;
    }

    public IStorageItem StorageItem
    {
        get; private set;
    }

    public DosShellItem(IStorageItem storageItem)
    {
        StorageItem = storageItem;

        isFolder = storageItem.IsOfType(StorageItemTypes.Folder);

        if (this.IsFolder)
        {
            IconId iconId = new IconId(0x0);
        }
        else
        {
            IconId iconId = new IconId(0x1);
        }

        OnPropertyChanged(nameof(IconId));
        OnPropertyChanged(nameof(ShellIcon));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    #region IEquatable<DosShellItem?>

    public override bool Equals(object? obj) => Equals(obj as DosShellItem);
    public bool Equals(DosShellItem? other) => other is not null &&
        EqualityComparer<IStorageItem>.Default.Equals(StorageItem, other.StorageItem) &&
        EqualityComparer<IStorageItem>.Default.Equals(StorageItem, other.StorageItem);

    public static bool operator ==(DosShellItem? left, DosShellItem? right) => EqualityComparer<DosShellItem>.Default.Equals(left, right);

    public static bool operator !=(DosShellItem? left, DosShellItem? right) => !(left == right);

    #endregion
}
