namespace GamePal.Models.AuthContracts
{
    public record AuthRequest(string EmailOrUsername, string Password);
}
