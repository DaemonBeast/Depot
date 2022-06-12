using Depot.Core;
using Depot.Storage.Tests;

namespace Depot.Storage.Local.Tests;

public class LocalStorageFixture : StorageFixtureBase
{
    protected override IStorage CreateInstance() => new LocalStorage();
}