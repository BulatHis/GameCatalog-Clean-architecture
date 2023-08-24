using GameCatalogDomain.DTO_s.JWT;

namespace TestProject1
{
    public class GenerateTokenJwtTests
    {
        [Test]
        public void Handle_WhenUserNotFound_ThrowsArgumentException()
        {
            // тест проверяет, что при обработке запроса на создание JWT-токена, если пользователь не найден выбрасывается исключение
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var generateTokenJwt = new GenerateTokenJwt(configurationMock.Object, userRepositoryMock.Object);

            var request = new CreateUserJwtRequest
            {
                Id = Guid.NewGuid(),
                Role = "Admin"
            };

            userRepositoryMock.Setup(repo => repo.Check(request.Id)).ReturnsAsync((User)null);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await generateTokenJwt.Handle(request);
            });
        }
    }
}