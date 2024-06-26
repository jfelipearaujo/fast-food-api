﻿using Domain.UseCases.Clients.Common.Responses;

namespace Web.Api.Endpoints.Clients.Mapping;

public static class ClientEndpointResponseMapper
{
    public static ClientEndpointResponse MapToResponse(this ClientResponse client)
    {
        return new ClientEndpointResponse
        {
            Id = client.Id,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            DocumentId = client.DocumentId,
            DocumentType = client.DocumentType,
            IsAnonymous = client.IsAnonymous,
            CreatedAtUtc = client.CreatedAtUtc,
            UpdatedAtUtc = client.UpdatedAtUtc,
        };
    }

    public static List<ClientEndpointResponse> MapToResponse(this List<ClientResponse> clients)
    {
        var response = new List<ClientEndpointResponse>();

        foreach (var client in clients)
        {
            response.Add(client.MapToResponse());
        }

        return response;
    }
}
