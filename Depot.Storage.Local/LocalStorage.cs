using Depot.Core;

namespace Depot.Storage.Local;

public class LocalStorage : IStorage
{
    public IDirectory Directory { get; }

    public IFile File { get; }

    public LocalStorage()
    {
        this.Directory = new LocalDirectory();
        this.File = new LocalFile();
    }
}