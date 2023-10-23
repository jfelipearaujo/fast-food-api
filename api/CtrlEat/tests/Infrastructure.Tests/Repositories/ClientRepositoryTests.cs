using Domain.Entities.ClientAggregate;
using Domain.Entities.ClientAggregate.ValueObjects;

using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Utils.Tests.Builders.Domain.Entities;

namespace Infrastructure.Tests.Repositories;

public class ClientRepositoryTests : IClassFixture<CustomDbFactory>, IDisposable
{
    private readonly ClientRepository sut;

    private readonly AppDbContext dbContext;

    private readonly DbContextOptions<AppDbContext> dbContextOptions;

    public ClientRepositoryTests(CustomDbFactory customDbFactory)
    {
        dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(customDbFactory.DbContainer.GetConnectionString())
            .Options;

        dbContext = new AppDbContext(dbContextOptions);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();

        sut = new ClientRepository(dbContext);
    }

    [Fact]
    public async Task ShouldCreateClientSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        // Act
        var response = await sut.CreateAsync(client, cancellationToken: default);

        // Assert
        response.Should().Be(1);
        dbContext.Client.Should().NotBeNullOrEmpty();

        var clientOnDb = await dbContext.Client.FindAsync(client.Id);

        clientOnDb.Should().NotBeNull().And.BeEquivalentTo(client);
    }

    [Fact]
    public async Task ShouldDeleteClientSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        await sut.CreateAsync(client, cancellationToken: default);

        // Act
        var response = await sut.DeleteAsync(client, cancellationToken: default);

        // Assert
        response.Should().Be(1);
        dbContext.Client.Should().BeNullOrEmpty();

        var clientOnDb = await dbContext.Client.FindAsync(client.Id);

        clientOnDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetAllClientsSuccessfully()
    {
        // Arrange
        var clientOne = new ClientBuilder()
            .WithSample()
            .WithEmail("client_01@email.com")
            .WithDocumentId("25106801028")
            .Build();

        var clientTwo = new ClientBuilder()
            .WithSample()
            .WithEmail("client_02@email.com")
            .WithDocumentId("39954655018")
            .Build();

        await sut.CreateAsync(clientOne, cancellationToken: default);
        await sut.CreateAsync(clientTwo, cancellationToken: default);

        // Act
        var response = await sut.GetAllAsync(cancellationToken: default);

        // Assert
        response.Should().NotBeNullOrEmpty().And.BeEquivalentTo(new List<Client>
        {
            clientOne,
            clientTwo
        });

        dbContext.Client.Count().Should().Be(2);
    }

    [Fact]
    public async Task ShouldGetByDocumentIdSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        await sut.CreateAsync(client, cancellationToken: default);

        // Act
        var response = await sut.GetByDocumentIdAsync(client.DocumentId, cancellationToken: default);

        // Assert
        response.Should().BeEquivalentTo(client);
    }

    [Fact]
    public async Task ShouldGetByEmailSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        await sut.CreateAsync(client, cancellationToken: default);

        // Act
        var response = await sut.GetByEmailAsync(client.Email, cancellationToken: default);

        // Assert
        response.Should().BeEquivalentTo(client);
    }

    [Fact]
    public async Task ShouldGetByIdSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        await sut.CreateAsync(client, cancellationToken: default);

        // Act
        var response = await sut.GetByIdAsync(client.Id, cancellationToken: default);

        // Assert
        response.Should().BeEquivalentTo(client);
    }

    [Fact]
    public async Task ShouldUpdateClientSuccessfully()
    {
        // Arrange
        var client = new ClientBuilder().WithSample().Build();

        await sut.CreateAsync(client, cancellationToken: default);

        client.Update(Email.Create("joao.silva@email.com.br").Value);

        // Act
        var response = await sut.UpdateAsync(client, cancellationToken: default);

        // Assert
        response.Should().Be(1);
        dbContext.Client.Should().NotBeNullOrEmpty();

        var clientOnDb = await dbContext.Client.FindAsync(client.Id);

        clientOnDb.Should().NotBeNull().And.BeEquivalentTo(client);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            dbContext.Dispose();
        }
    }
}
