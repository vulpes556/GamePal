using GamePal.DTOs.Requests;
using GamePal.Models.AuthContracts;

namespace GamePal.Services.UserServices
{
    public interface IUserService
    {
        Task<AuthResult> RegisterAsync(RegistrationRequest registerRequest, string role);
        Task<AuthResult> LoginAsync(AuthRequest authRequest);
        Task<AuthResult> UpsertUserAsync(ExternalAuthRequest extAuthReq);
        public Task AddGameToUserLibraryAsync(string userId, AddGameToUserDto addGameRequest);

    }
}
