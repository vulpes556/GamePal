namespace GamePal.Data.DataEnums
{
    public static class Enums
    {
        [Flags]
        public enum PlayDays
        {
            None = 0,
            Monday = 1 << 0,
            Tuesday = 1 << 1,
            Wednesday = 1 << 2,
            Thursday = 1 << 3,
            Friday = 1 << 4,
            Saturday = 1 << 5,
            Sunday = 1 << 6
        }

        public enum PlayStyle
        {
            Casual,
            Midcore,
            Hardcore,
            Competetive,
        }

    }
}
