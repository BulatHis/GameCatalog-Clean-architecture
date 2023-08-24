namespace TestProject1
{
    public class HashPasswordsTests
    {
        [Test] //проверка работы создания и проверки паролей 
        public void VerifyPassword_WhenPasswordsMatch_ReturnsTrue()
        {
            // Arrange
            string password = "password123";
            string hash = new HashPasswords().Hash(password);

            // Act
            bool result = new HashPasswords().VerifyPassword(password, hash);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void VerifyPassword_WhenPasswordsDoNotMatch_ReturnsFalse()
        {
            // Arrange
            string password = "password123";
            string incorrectPassword = "wrongpassword";
            string hash = new HashPasswords().Hash(password);

            // Act
            bool result = new HashPasswords().VerifyPassword(incorrectPassword, hash);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
