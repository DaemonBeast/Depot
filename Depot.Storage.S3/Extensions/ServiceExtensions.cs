using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Depot.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Depot.Storage.S3.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddS3Storage(
        this IServiceCollection services,
        string accessKeyId,
        string secretAccessKey,
        string serviceUrl,
        string bucketName = "depot")
    {
        services.AddAWSService<IAmazonS3>(new AWSOptions()
        {
            Credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey),
            DefaultClientConfig =
            {
                ServiceURL = serviceUrl
            }
        });

        services.AddSingleton<IStorage, S3Storage>(provider =>
            ActivatorUtilities.CreateInstance<S3Storage>(provider, bucketName));

        return services;
    }
}