using Amazon.S3;
using Amazon.S3.Transfer;

using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
using Domain.Adapters.Storage.Responses;

namespace Application.Services.StorageService;

public class StorageService : IStorageService
{
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
