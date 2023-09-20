﻿using Domain.Entities;
using Domain.Enums;

using Infrastructure.Repositories;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data.Common;

namespace Infrastructure.Tests.Repositories
{
    public class ClientRepositoryTests : IDisposable
    {
        private readonly ClientRepository sut;

        private readonly AppDbContext dbContext;

        private readonly DbConnection dbConnection;

        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public ClientRepositoryTests()
        {
            dbConnection = new SqliteConnection("Filename=:memory:");
            dbConnection.Open();

            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(dbConnection)
                .Options;

            dbContext = new AppDbContext(dbContextOptions);

            dbContext.Database.Migrate();

            sut = new ClientRepository(dbContext);
        }

        [Fact]
        public async Task ShouldCreateClientSuccessfully()
        {
            // Arrange
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

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
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

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
            var clientOne = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

            var clientTwo = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Luis",
                LastName = "Silva",
                Email = "luis.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "89414432027",
                IsAnonymous = false,
            };

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
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

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
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

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
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

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
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@email.com",
                DocumentType = DocumentType.CPF,
                DocumentId = "46808459029",
                IsAnonymous = false,
            };

            await sut.CreateAsync(client, cancellationToken: default);

            client.Email = "joao.silva@email.com.br";

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
                dbConnection.Dispose();
                dbContext.Dispose();
            }
        }
    }
}