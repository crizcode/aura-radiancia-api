using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Moq;
using Xunit;

namespace Aura.UnitTests.Interfaces
{
    public class IAuthRepositoryTests
    {
        [Fact]
        public async Task SearchPersonByCredentialsAsync_Should_Return_Person_When_Credentials_Are_Valid()
        {
            // Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var cancellationToken = CancellationToken.None;

            var expectedPerson = new Person(); // Simulated Person object to return

            mockAuthRepository.Setup(repo => repo.SearchPersonByCredentialsAsync("username", "password", cancellationToken))
                              .ReturnsAsync(expectedPerson);

            var authRepository = mockAuthRepository.Object;

            // Act
            var result = await authRepository.SearchPersonByCredentialsAsync("username", "password", cancellationToken);

            // Assert
            Assert.Equal(expectedPerson, result);
        }

    }
}