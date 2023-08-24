using GameCatalogInteractor.EmailCode.SecretKey;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GameCatalogInteractor.EmailCode;

public class MailHandler : IMailHandler
{
    private readonly IConfiguration _configuration;
    private readonly ICodeGenerator _codeGenerator;

    public MailHandler(IConfiguration configuration, ICodeGenerator сodeGenerator)
    {
        _configuration = configuration;
        _codeGenerator = сodeGenerator;
    }

    public async Task SendEmailAsync(string email)
    {
        var code = _codeGenerator.GenerateCode(email);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration["MailSettings:DisplayName"],
            _configuration["MailSettings:Mail"]));
        message.To.Add(new MailboxAddress("Client", email));
        message.Subject = "Code";

        message.Body = new TextPart("plain")
        {
            Text = $"Code {code}"
        };

        using (var client = new SmtpClient())
        {
            var port = Convert.ToInt32(_configuration["MailSettings:Port"]);
            var domain = _configuration["MailSettings:Host"];
            var authPassword = _configuration["MailSettings:APIkey"];
            var authName = _configuration["MailSettings:Mail"];
            await client.ConnectAsync(domain, port, true);
            await client.AuthenticateAsync(authName, authPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

public interface IMailHandler
{
    Task SendEmailAsync(string email);
}