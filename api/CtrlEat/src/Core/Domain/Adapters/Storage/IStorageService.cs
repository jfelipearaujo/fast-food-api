using Domain.Adapters.Storage.Requests;
using Domain.Adapters.Storage.Responses;

namespace Domain.Adapters.Storage;

public interface IStorageService
{
    Task<UploadObjectResponse> UploadFileAsync(UploadObjectRequest request, CancellationToken cancellationToken);
    Task<DownloadObjectResponse> DownloadFileAsync(DownloadObjectRequest request, CancellationToken cancellationToken);
}
