using Depot.Core;
using Depot.Storage.Local;
using Depot.Storage.Tests;

namespace Depot.Storage.Multi.Tests;

public class MultiStorageFixture : StorageFixtureBase
{
    protected override IStorage CreateInstance() => new MultiStorage(new[]
    {
        new LocalStorage()
    });
}