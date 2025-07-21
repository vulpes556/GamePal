using GamePal.DTOs.Requests;
using GamePal.Models.GameModels;

namespace GamePal.Services.UserGameServices
{
    public interface IUserGameService
    {
        public Task<IEnumerable<UserGameDTO>> GetAllWithoutUsersAsync();

    }
}
