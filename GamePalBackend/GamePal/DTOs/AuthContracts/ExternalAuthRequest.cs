
namespace GamePal.Models.AuthContracts
{
    public record ExternalAuthRequest(
           string ProviderName,
           string ProviderAccountId,
           string Email,
           string Name,
           string Image);
}
