using GamePal.Data.Entities;

namespace LadleMeThis.Services.TokenService
{
    public interface ITokenService
    {
        public Task<string> CreateToken(User user);
    }
}
