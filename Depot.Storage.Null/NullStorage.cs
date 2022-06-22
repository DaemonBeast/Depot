using Depot.Core;

namespace Depot.Storage.Null;

public class NullStorage : IStorage
{
    public IDirectory Directory { get; }

    public IFile File { get; }

    public NullStorage()
    {
        this.Directory = new NullDirectory();
        this.File = new NullFile();
    }
}