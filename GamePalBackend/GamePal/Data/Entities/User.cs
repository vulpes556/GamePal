using Microsoft.AspNetCore.Identity;

namespace GamePal.Data.Entities
{
    public class User : IdentityUser
    {

        public Country Country { get; set; }
        public GamingPreference GamingPreference { get; set; }
        public ICollection<Game> Games { get; set; }

    }
}
