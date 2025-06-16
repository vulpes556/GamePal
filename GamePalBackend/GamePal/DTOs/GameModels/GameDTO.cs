using GamePal.DTOs;

namespace GamePal.Models.GameModels
{
    public class GameDTO
    {
        //public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<PlatformDTO> Platforms { get; set; }
        public ICollection<GameCategoryDTO> Categories { get; set; }
    }
}
