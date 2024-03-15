using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
using Domain.Adapters.Storage.Responses;

using System.Net;

namespace Application.Services.StorageService;

public class StorageService : IStorageService
{
    public async Task<DownloadObjectResponse> DownloadFileAsync(
        DownloadObjectRequest request,
        CancellationToken cancellationToken)
    {
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
                    response.StatusCode = 404;
                    response.Message = $"{request.Name} could not be downloaded";
                    return response;
                }

                response.FileStream = ms;
                response.StatusCode = 200;
                response.Message = $"{request.Name} has been downloaded sucessfully";
            }
            else
            {
                response.StatusCode = (int)getObjectResponse.HttpStatusCode;
                response.Message = $"{request.Name} could not be downloaded";
            }
        }
        catch (AmazonS3Exception s3Ex)
        {
            response.StatusCode = (int)s3Ex.StatusCode;
            response.Message = s3Ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<UploadObjectResponse> UploadFileAsync(
        UploadObjectRequest request,
        CancellationToken cancellationToken)
    {
        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1
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
            using var transferUtility = new TransferUtility(client);

            await transferUtility.UploadAsync(uploadRequest, cancellationToken);

            response.Url = request.BucketName + "/" + request.Name;
            response.StatusCode = 201;
            response.Message = $"{request.Name} has been uploaded sucessfully";
        }
        catch (AmazonS3Exception s3Ex)
        {
            response.StatusCode = (int)s3Ex.StatusCode;
            response.Message = s3Ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Message = ex.Message;
        }

        return response;
    }
}
