using Amazon.S3;
using Depot.Core;

namespace Depot.Storage.S3;

public class S3Storage : IStorage
{
    public IDirectory Directory { get; }

    public IFile File { get; }

    internal readonly IAmazonS3 S3Client;
    internal readonly string BucketName;

    public S3Storage(IAmazonS3 s3Client, string bucketName)
    {
        this.S3Client = s3Client;
        this.BucketName = bucketName;
        
        this.Directory = new S3Directory(this);
        this.File = new S3File(this);
    }

    internal async Task CreateBucketIfNotExistsAsync()
    {
        if (!await this.S3Client.DoesS3BucketExistAsync(this.BucketName))
        {
            await this.S3Client.PutBucketAsync(this.BucketName);
        }
    }
}