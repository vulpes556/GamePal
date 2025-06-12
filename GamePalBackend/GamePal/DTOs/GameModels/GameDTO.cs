using GamePal.Data.DataEnums;

namespace GamePal.Models.GameModels
{
    public class GameDTO
    {
        //public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<string> Platforms { get; set; }
        public ICollection<string> Categories { get; set; }
    }
}
