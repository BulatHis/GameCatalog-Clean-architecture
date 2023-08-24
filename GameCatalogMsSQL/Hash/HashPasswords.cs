namespace GameCatalogMsSQL.Hash;

public class HashPasswords : IHashPasswords
{
    public string Hash(string secret)
    {
        return BCrypt.Net.BCrypt.HashPassword(secret);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}


public interface IHashPasswords
{
    string Hash(string email);
    bool VerifyPassword(string password, string hash);
}