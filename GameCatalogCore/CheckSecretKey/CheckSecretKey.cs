using System.Text;
using OtpNet;

namespace GameCatalogCore.CheckSecretKey;

public class CheckSecretKey : ICheckSecretKey
{
    public bool CheckKey(string seckretWord, string mailCode)
    {
        var totp = new Totp(Encoding.UTF8.GetBytes(seckretWord), mode: OtpHashMode.Sha256, step: 90);
        return totp.VerifyTotp(mailCode, out long a);
    }
}

public interface ICheckSecretKey
{
    public bool CheckKey(string keyWord, string mailCode);
}