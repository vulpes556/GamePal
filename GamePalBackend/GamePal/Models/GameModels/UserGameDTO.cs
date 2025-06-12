namespace GamePal.Models.GameModels
{
    public class UserGameDTO
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Platform { get; set; }
        public ICollection<string> Categories { get; set; }
    }
}
