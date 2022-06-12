using System;
using Depot.Core;
using Depot.Storage.Tests;

namespace Depot.Storage.S3.Tests;

public class S3StorageFixture : StorageFixtureBase
{
    protected override IStorage CreateInstance() => /* new S3Storage() */
        throw new Exception("Must provide S3 credentials for S3 storage instance");
}