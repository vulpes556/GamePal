using GamePal.Data.DataEnums;

namespace GamePal.Models.GameModels
{
    public class GameDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
    }
}
