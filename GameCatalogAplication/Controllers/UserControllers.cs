using GameCatalogCore.DTO_s.UserDTO_s;
using GameCatalogCore.Filters;
using GameCatalogCore.IServices;
using GameCatalogDomain.DTO_s.UserDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAplication.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    /// <summary>
    /// Обновить данные пользователя(пока не используется,с начала  нужно добавить профиль польз-ля)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Вы не авторизованы</response>
    /// <response code="500">Ошибка при обновление данных</response>
    [HttpPost]
    [ExecutionTime]
    [Route("updateuser"), Authorize("User")]
    public async Task<UpdateUserDtoResponse> UpdateUser(UpdateUserDtoRequest request)
    {
        return await _userServices.UpdateUser(request);
    }

    /// <summary>
    /// Проверить поль-ля по email (используется при легине и проверке почты)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Вы не авторизованы</response>
    /// <response code="500">Поль-ль не найден (ошибка сервера)</response>
    [HttpGet]
    [ExecutionTime]
    [Route("checkuser")]
    public async Task<GetUserDtoResponse> CheckUser([FromQuery] GetUserDtoRequest request)
    {
        return await _userServices.CheckUser(request);
    }
    
    /// <summary>
    /// Удалить поль-ля (пока не используется,с начала  нужно добавить профиль польз-ля)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Вы не авторизованы</response>
    /// <response code="500">Ошибка при удаление поль-ля(скорее всего - не найден)</response>
    [HttpPost]
    [ExecutionTime]
    [Route("deleteuser")]
    [Authorize(Roles = "User,Admin")]
    public async Task<DeleteUserDtoResponse> Delete(DeleteUserDtoRequest request)
    {
        return await _userServices.DeleteUser(request);
    }

    /// <summary>
    /// Регистрация также внутри высылается код на почту, которую задал поль-ль (если ошибка при создание поль-ля
    /// пароль не отправится, также добовляет jwt токен к ответу)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при регистрации</response>
    [HttpPost]
    [CustomExceptionFilter]
    [ExecutionTime]
    [Route("registration")]
    public async Task<RegistrationDtoResponse> Registration(RegistrationDtoRequest request)
    {
        return await _userServices.Registration(request);
    }


    /// <summary>
    /// Вход (проверяет наличие пользователя и правильность пароля + отправляет jwt токен)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при входе (пароль или логи неверны)</response>
    [HttpGet("login")]
    [CustomExceptionFilter]
    [ExecutionTime]
    public async Task<LoginUserDtoResponse> Login([FromQuery] LoginUserDtoRequest request)
    {
        return await _userServices.Login(request);
    }

    /// <summary>
    /// Подтверждает пользователя исходя из его кода с email 
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при подтверждение(код неверен)</response>
    [HttpGet]
    [ExecutionTime]
    [Route("confirmuser")]
    public async Task<ConfirmUserDtoResponse> Confirm([FromQuery] ConfirmUserDtoRequest request)
    {
        return await _userServices.ConfirmUser(request);
    }


    /// <summary>
    /// Отправляет код для подтверждения поль-ля на его email
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при отправке кода(не валидный email)</response>
    [HttpGet]
    [ExecutionTime]
    [Route("getmail")]
    public async Task GetMail([FromQuery] string email)
    {
        await _userServices.GetMailKey(email);
    }

    /// <summary>
    /// Обновляет refresh token на сайте, чтобы авторизованные поль-ли имели свои права(продлеваем сессию)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при создание refresh токена</response>
    [HttpPost]
    [ExecutionTime]
    [Route("refreshtoken")]
    public async Task<RefreshTokenDtoResponse> Refresh(RefreshTokenDtoRequest request)
    {
        return await _userServices.Refresh(request);
    }
}