
using GameCatalogInteractor.EmailCode.SecretKey;

namespace TestProject1;
//эти тесты проверяют подтверждение email кода, который приходит клиенту

[TestFixture]
public class CodeGeneratorTests 
{ 
    [Test] //проверяет создание кода
    public void GenerateCode_ReturnsValidCode()
    {
        string email = "test@example.com";
        string secretWord = "mySecretWord";

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(new User { SecretKey = secretWord });

        var codeGenerator = new CodeGenerator(userRepositoryMock.Object);

        // Act
        string code = codeGenerator.GenerateCode(email);

        // Assert
        Assert.IsNotNull(code);
        Assert.IsNotEmpty(code);
    }
}

[TestFixture]
public class CheckSecretKeyTests
{
    [Test]//данный тест проверяет код, который будет введен поль-лем при проверке почты 
    // начала мы генерируем ключ с помощью почты (внутри мокаем репозиторий и получаем секретный ключ поль-ля)
    //заетм просто проверям код и все
    public void CheckKey_WhenValidCode_ReturnsTrue()
    {
        // Arrange
        string secretWord = "yYFhZHKrrJYgwuDPmRWG8OQXNEE=";
        string email = "bulatxisameev@gmail.com";
        
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(new User { SecretKey = secretWord });
        var codeGenerator = new CodeGenerator(userRepositoryMock.Object);
        string code = codeGenerator.GenerateCode(email);
        
        var checkSecretKey = new CheckSecretKey();

        // Act
        bool result = checkSecretKey.CheckKey(secretWord, code);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]//проверяет, что какой-то случайны код не пройдет
    public void CheckKey_WhenInvalidCode_ReturnsFalse()
    {
        // Arrange
        string secretWord = "mySecretWord";
        string mailCode = "654321";

        var checkSecretKey = new CheckSecretKey();

        // Act
        bool result = checkSecretKey.CheckKey(secretWord, mailCode);

        // Assert
        Assert.IsFalse(result);
    }
}