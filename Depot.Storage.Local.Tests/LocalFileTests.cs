using Depot.Storage.Tests;

namespace Depot.Storage.Local.Tests;

public class LocalFileTests : FileTestBase<LocalStorageFixture>
{
    public LocalFileTests(LocalStorageFixture fixture) : base(fixture) {}
}