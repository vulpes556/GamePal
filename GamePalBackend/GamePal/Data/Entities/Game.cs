using GamePal.Data.DataEnums;

namespace GamePal.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enums.Platform Platform { get; set; }
    }
}
