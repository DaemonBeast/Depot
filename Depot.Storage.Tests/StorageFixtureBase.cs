using Depot.Core;

namespace Depot.Storage.Tests;

public abstract class StorageFixtureBase
{
    public IStorage Storage => _storage ??= this.CreateInstance();

    private IStorage? _storage;

    protected abstract IStorage CreateInstance();
}