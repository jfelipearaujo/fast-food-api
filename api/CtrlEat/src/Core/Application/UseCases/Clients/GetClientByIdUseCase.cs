using Domain.Adapters;
using Domain.Errors.Clients;
using Domain.UseCases.Clients;
using Domain.UseCases.Clients.Requests;
using Domain.UseCases.Clients.Responses;

using FluentResults;

namespace Application.UseCases.Clients
{
    public class GetClientByIdUseCase : IGetClientByIdUseCase
    {
        private readonly IClientRepository repository;

        public GetClientByIdUseCase(IClientRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<ClientResponse>> ExecuteAsync(
            GetClientByIdRequest request,
            CancellationToken cancellationToken)
        {
            var client = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (client is null)
            {
                return Result.Fail(new ClientNotFoundError(request.Id));
            }

            return ClientResponse.MapFromDomain(client);
        }
    }
}
