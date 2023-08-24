using GameCatalogCore.DTO_s.UserDTO_s;
using GameCatalogDomain.DTO_s.UserDTO_s;

namespace GameCatalogCore.IServices;

public interface IUserServices
{
    Task<UpdateUserDtoResponse> UpdateUser(UpdateUserDtoRequest request);
    Task<GetUserDtoResponse> CheckUser(GetUserDtoRequest request);
    Task<DeleteUserDtoResponse> DeleteUser(DeleteUserDtoRequest request);
    
    Task<RegistrationDtoResponse> Registration(RegistrationDtoRequest request);
    
    Task<LoginUserDtoResponse> Login(LoginUserDtoRequest request);
    
    Task<ConfirmUserDtoResponse> ConfirmUser(ConfirmUserDtoRequest request);
    
    Task GetMailKey(string email);
    Task<RefreshTokenDtoResponse> Refresh(RefreshTokenDtoRequest request);
}