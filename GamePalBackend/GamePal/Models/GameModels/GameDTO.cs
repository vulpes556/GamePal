using GamePal.Data.DataEnums;

namespace GamePal.Models.GameModels
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enums.Platform Platform { get; set; }
    }
}
