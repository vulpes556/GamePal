using GamePal.Data.DataEnums;
using GamePal.Models.GameModels;

namespace GamePal.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameCategory> Categories { get; set; }
        public string? ImageUrl { get; set; } = null;
        public ICollection<Platform> Platforms { get; set; }

    }
}
