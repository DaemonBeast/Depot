using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Depot.Core;

namespace Depot.Storage.S3;

public class S3Directory : IDirectory
{
    private readonly S3Storage _s3Storage;
    
    public S3Directory(S3Storage s3Storage)
    {
        this._s3Storage = s3Storage;
    }
    
    public async Task CreateAsync(string directoryPath)
    {
        await this._s3Storage.CreateBucketIfNotExistsAsync();

        var request = new PutObjectRequest()
        {
            BucketName = this._s3Storage.BucketName,
            Key = AddTrailingSlash(directoryPath)
        };

        await this._s3Storage.S3Client.PutObjectAsync(request);
    }

    public async Task DeleteAsync(string directoryPath)
    {
        await this._s3Storage.S3Client.DeleteObjectAsync(this._s3Storage.BucketName, AddTrailingSlash(directoryPath));
    }

    public async Task<bool> ExistsAsync(string directoryPath)
    {
        try
        {
            await this._s3Storage.S3Client.GetObjectMetadataAsync(
                this._s3Storage.BucketName,
                AddTrailingSlash(directoryPath));

            return true;
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            
            throw;
        }
    }

    private static string AddTrailingSlash(string directoryPath)
    {
        const string s3DirectorySeparator = "/";
        
        return directoryPath.EndsWith(s3DirectorySeparator)
            ? directoryPath
            : Path.TrimEndingDirectorySeparator(directoryPath) + s3DirectorySeparator;
    }
}