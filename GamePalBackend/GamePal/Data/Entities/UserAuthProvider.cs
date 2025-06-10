namespace GamePal.Data.Entities
{
    public class UserAuthProvider
    {
        public int Id { get; set; }
        public AuthProvider AuthProvider { get; set; }
        public User User { get; set; }

        //    The *external* ID that the OAuth provider gives for this user.
        //    For GitHub, this is their numeric/string "id" (e.g. "1234567").
        public string ProviderUserId { get; set; }

    }
}
