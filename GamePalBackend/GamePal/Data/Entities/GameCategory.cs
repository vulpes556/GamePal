namespace GamePal.Data.Entities
{
    public class GameCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
