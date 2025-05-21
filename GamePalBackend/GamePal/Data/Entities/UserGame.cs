namespace GamePal.Data.Entities
{
    public class UserGame
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
        public Platform Platform { get; set; }

    }
}
