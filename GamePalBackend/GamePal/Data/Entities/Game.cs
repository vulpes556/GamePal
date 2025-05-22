using GamePal.Data.DataEnums;

namespace GamePal.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameCategory> Categories { get; set; }
    }
}
