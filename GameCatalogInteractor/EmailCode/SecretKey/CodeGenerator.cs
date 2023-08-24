using System.Text;
using GameCatalogMsSQL.IRepositories;
using OtpNet;

namespace GameCatalogInteractor.EmailCode.SecretKey;

public class CodeGenerator : ICodeGenerator
{
    private readonly IUserRepository _userRepository;

    public CodeGenerator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string  GenerateCode(string email)
    {
        var word = _userRepository.GetByEmail(email).Result.SecretKey;
        var totp = new Totp(Encoding.UTF8.GetBytes(word),mode: OtpHashMode.Sha256, step:90);
        return totp.ComputeTotp();
    }
}

public interface ICodeGenerator
{
    public string  GenerateCode(string email);
    
}