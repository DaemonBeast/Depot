using Depot.Core;

namespace Depot.Storage.Multi;

public class MultiStorage : IMultiStorage
{
    public IDirectory Directory { get; }

    public IFile File { get; }
    
    internal readonly IStorage[] Storages;

    public MultiStorage(IEnumerable<IStorage> storages)
    {
        this.Storages = storages.ToArray();

        this.Directory = new MultiDirectory(this);
        this.File = new MultiFile(this);
    }

    internal async Task Execute(Func<IStorage, Task> taskFactory, string exception)
    {
        foreach (var storage in this.Storages)
        {
            try
            {
                await taskFactory.Invoke(storage);

                return;
            }
            catch
            {
                // ignored
            }
        }

        throw new Exception(exception);
    }
    
    internal async Task<T> Execute<T>(Func<IStorage, Task<T>> taskFactory, string exception)
    {
        foreach (var storage in this.Storages)
        {
            try
            {
                return await taskFactory.Invoke(storage);
            }
            catch
            {
                // ignored
            }
        }

        throw new Exception(exception);
    }
}