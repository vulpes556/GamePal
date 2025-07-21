namespace GamePal.Models.AuthContracts
{
    public record AuthResult(
     string UserId,
     bool Success,
     string Email,
     string UserName,
     string Token
        )
    {
        //Error code - error message
        public readonly Dictionary<string, string> ErrorMessages = new();
    }
}
