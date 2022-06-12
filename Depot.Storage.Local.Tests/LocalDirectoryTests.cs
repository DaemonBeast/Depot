using Depot.Storage.Tests;

namespace Depot.Storage.Local.Tests;

public class LocalDirectoryTests : DirectoryTestBase<LocalStorageFixture>
{
    public LocalDirectoryTests(LocalStorageFixture fixture) : base(fixture) {}
}