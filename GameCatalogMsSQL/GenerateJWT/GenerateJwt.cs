using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameCatalogCore.DTO_s.JWT;
using GameCatalogDomain.DTO_s.JWT;
using GameCatalogMsSQL.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameCatalogMsSQL.GenerateJWT;

public class GenerateTokenJwt : IGenerateToken
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public GenerateTokenJwt(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }


    public async Task<CreateUserJwtResponse> Handle(CreateUserJwtRequest request)
    {
        var accessToken = GenerateAccessToken(request);
        var refreshToken = Guid.NewGuid().ToString();
        var user = await _userRepository.Check(request.Id);
        if (user == null)
        {
            throw new ArgumentException($"User with {request.Id} not found");
        }

        user.RefreshToken = refreshToken;
        await _userRepository.AddRefreshToken(user);
        return new CreateUserJwtResponse()
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public string GenerateAccessToken(CreateUserJwtRequest request)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]  ?? "DefaultSecretKey"));
        var claims = new List<Claim>
        {
            new Claim("id", request.Id.ToString()),
            new Claim(ClaimTypes.Role, request.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow
                .AddMinutes(Convert.ToInt32(_configuration["JWT:AccessTokenExpirationMinutes"])),
            SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(jwtToken);
        return accessToken;
    }
}

public interface IGenerateToken
{
    public Task<CreateUserJwtResponse> Handle(CreateUserJwtRequest request);
    public string GenerateAccessToken(CreateUserJwtRequest request);
}