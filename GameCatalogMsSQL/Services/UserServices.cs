using System.Security.Cryptography;
using GameCatalogCore.DTO_s.JWT;
using GameCatalogCore.DTO_s.UserDTO_s;
using GameCatalogCore.IServices;
using GameCatalogDomain.DTO_s.JWT;
using GameCatalogDomain.DTO_s.UserDTO_s;
using GameCatalogDomain.Entity;
using GameCatalogInteractor.EmailCode;
using GameCatalogInteractor.EmailCode.SecretKey;
using GameCatalogMsSQL.GenerateJWT;
using GameCatalogMsSQL.Hash;
using GameCatalogMsSQL.IRepositories;

namespace GameCatalogMsSQL.Services;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IGenerateToken _generateToken;
    private readonly IHashPasswords _hashPasswords;
    private readonly ICheckSecretKey _checkSecretKey;
    private readonly IMailHandler _mailHandler;

    public UserServices(IUserRepository userRepository, IGenerateToken generateToken, IHashPasswords hashPasswords,
        ICheckSecretKey checkSecretKey, IMailHandler mailHandler)
    {
        _userRepository = userRepository;
        _generateToken = generateToken;
        _hashPasswords = hashPasswords;
        _checkSecretKey = checkSecretKey;
        _mailHandler = mailHandler;
    }

    public async Task<UpdateUserDtoResponse> UpdateUser(UpdateUserDtoRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user == null)
        {
            return new UpdateUserDtoResponse() { Result = false };
        }

        user.Email = request.Email;
        user.Name = request.Name;
        await _userRepository.Update(user);
        return new UpdateUserDtoResponse() { Result = true };
    }

    public async Task<GetUserDtoResponse> CheckUser(GetUserDtoRequest request)
    {
        var res = await _userRepository.GetByEmail(request.Email);
        return new GetUserDtoResponse() { Email = res.Email, Name = res.Name };
    }

    public async Task<DeleteUserDtoResponse> DeleteUser(DeleteUserDtoRequest request)
    {
        var user = await _userRepository.Check(request.Id);
        if (user == null) return new DeleteUserDtoResponse() { Result = false };
        await _userRepository.Delete(user);
        return new DeleteUserDtoResponse() { Result = true };
    }

    public async Task<RegistrationDtoResponse> Registration(RegistrationDtoRequest request)
    {
        if (_userRepository.GetByEmail(request.Email).Result != null)
        {
            throw new ArgumentException($" email {request.Email} уже существует");
        }

        var hashedPassword = _hashPasswords.Hash(request.Password);
        var user = new User
        {
            Id = Guid.NewGuid(), Password = hashedPassword,
            SecretKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(20)), Email = request.Email,
            Name = request.Name, Role = "User", RefreshToken = Guid.NewGuid().ToString(),
            IsConfirmed = false
        };
        await _userRepository.Add(user);
        var jwt = await _generateToken.Handle(new CreateUserJwtRequest()
        {
            Id = user.Id,
            Role = user.Role
        });
        return new RegistrationDtoResponse()
            { Result = true, AccessToken = jwt.AccessToken, RefreshToken = jwt.RefreshToken };
    }

    public async Task<LoginUserDtoResponse> Login(LoginUserDtoRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user == null)
        {
            throw new ArgumentException($" email {request.Email} не существует");
        }

        var jwt = await _generateToken.Handle(new CreateUserJwtRequest()
        {
            Id = user.Id,
            Role = user.Role
        });
        return _hashPasswords.VerifyPassword(request.Password, user.Password) //Карина <3
            ? new LoginUserDtoResponse() { Result = false }
            : new LoginUserDtoResponse()
            {
                Result = true, AccessToken = jwt.AccessToken, RefreshToken = jwt.RefreshToken,
                IsConfirmed = user.IsConfirmed, UserId = user.Id
            };
    }

    public async Task<ConfirmUserDtoResponse> ConfirmUser(ConfirmUserDtoRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user.Email == null) return new ConfirmUserDtoResponse() { Result = false };
        var keyWord = user.SecretKey;
        if (!_checkSecretKey.CheckKey(keyWord, request.SeckretKey))
            return new ConfirmUserDtoResponse() { Result = false };
        await _userRepository.ConfirmUser(user);
        return new ConfirmUserDtoResponse() { Result = true, UserId = user.Id };
    }

    public async Task GetMailKey(string email)
    {
        await _mailHandler.SendEmailAsync(email);
    }

    public async Task<RefreshTokenDtoResponse> Refresh(RefreshTokenDtoRequest request)
    {
        var user = await _userRepository.CheckByRefreshToken(request.Refresh);
        if (user.RefreshToken == request.Refresh)
        {
            var jwt = await _generateToken.Handle(new CreateUserJwtRequest()
            {
                Id = user.Id,
                Role = user.Role
            });
            user.RefreshToken = jwt.RefreshToken;
            await _userRepository.Update(user);
            return new RefreshTokenDtoResponse()
            {
                RefreshToken = jwt.RefreshToken,
                AccessToken = jwt.AccessToken
            };
        }
        throw new ArgumentException($"you are not authorized");
    }
}