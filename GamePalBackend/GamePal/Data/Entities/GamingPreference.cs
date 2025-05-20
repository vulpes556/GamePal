using GamePal.Data.DataEnums;

namespace GamePal.Data.Entities
{
    public class GamingPreference
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Enums.PlayStyle PlayStyle { get; set; }
        public int WeeklyPlaytime { get; set; }
        public Enums.PlayDays ActiveDays { get; set; }
        public ICollection<Language> SpokenLanguages { get; set; }

    }
}
