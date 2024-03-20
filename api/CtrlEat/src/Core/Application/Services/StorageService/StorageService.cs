using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
using Domain.Adapters.Storage.Responses;

using Microsoft.Extensions.Logging;

using System.Net;

namespace Application.Services.StorageService;

public class StorageService : IStorageService
{
    private readonly ILogger<StorageService> logger;

    public StorageService(ILogger<StorageService> logger)
    {
        this.logger = logger;
    }

    public async Task<DownloadObjectResponse> DownloadFileAsync(
        DownloadObjectRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Downloading file {fileName} from S3", request.Name);

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1
        };

        var response = new DownloadObjectResponse();

        try
        {
            using var client = new AmazonS3Client(config);

            var getObjectRequest = new GetObjectRequest
            {
                BucketName = request.BucketName,
                Key = request.Name
            };

            using var getObjectResponse = await client.GetObjectAsync(getObjectRequest, cancellationToken);

            if (getObjectResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                using var ms = new MemoryStream();
                await getObjectResponse.ResponseStream.CopyToAsync(ms, cancellationToken);

                if (ms.Length == 0)
                {
                    logger.LogWarning("File {fileName} could not be downloaded from S3", request.Name);

                    response.StatusCode = 404;
                    response.Message = $"{request.Name} could not be downloaded";
                    return response;
                }

                logger.LogInformation("File {fileName} downloaded from S3", request.Name);

                response.FileData = ms.ToArray();
                response.StatusCode = 200;
                response.Message = $"{request.Name} has been downloaded sucessfully";
            }
            else
            {
                logger.LogWarning("File {fileName} could not be downloaded from S3, status {status}", request.Name, getObjectResponse.HttpStatusCode);

                response.StatusCode = (int)getObjectResponse.HttpStatusCode;
                response.Message = $"{request.Name} could not be downloaded";
            }
        }
        catch (AmazonS3Exception s3Ex)
        {
            logger.LogError(s3Ex, "Error downloading file {fileName} from S3", request.Name);

            response.StatusCode = (int)s3Ex.StatusCode;
            response.Message = s3Ex.Message;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error downloading file {fileName} from S3", request.Name);

            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<UploadObjectResponse> UploadFileAsync(
        UploadObjectRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading file {fileName} to S3", request.Name);

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1,
        };

        var response = new UploadObjectResponse();

        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = request.InputStream,
                Key = request.Name,
                BucketName = request.BucketName,
                CannedACL = S3CannedACL.NoACL
            };

            using var client = new AmazonS3Client(config);

            var listObjectsRequest = new ListObjectsRequest
            {
                BucketName = request.BucketName
            };

            logger.LogInformation("Listing files in bucket {bucketName}", request.BucketName);

            var listObjectsResponse = await client.ListObjectsAsync(listObjectsRequest, cancellationToken);

            foreach (var s3Object in listObjectsResponse.S3Objects)
            {
                logger.LogInformation("file: {fileName}", s3Object.Key);
            }

            using var transferUtility = new TransferUtility(client);

            logger.LogInformation("Uploading...");

            await transferUtility.UploadAsync(uploadRequest, cancellationToken);

            logger.LogInformation("File {fileName} uploaded to S3", request.Name);

            response.Url = request.BucketName + "/" + request.Name;
            response.StatusCode = 201;
            response.Message = $"{request.Name} has been uploaded sucessfully";
        }
        catch (AmazonS3Exception s3Ex)
        {
            logger.LogError(s3Ex, "S3 Error uploading file {fileName} to S3", request.Name);

            response.StatusCode = (int)s3Ex.StatusCode;
            response.Message = s3Ex.Message;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading file {fileName} to S3", request.Name);

            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
}
