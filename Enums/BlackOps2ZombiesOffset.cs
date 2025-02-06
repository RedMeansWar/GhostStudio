namespace GhostStudio.Enums
{
    public enum BlackOps2ZombiesStatsOffset : uint
    {
        StatEntry = 0x843489C8,
        BulletsTotal = 0x84348AD0,
        MilesTraveled = 0x84348ACC,
        Perks = 0x84348AA8,
        Revives = 0x84348AA4,
        XP = 0x84348C00,
        Kills = 0x84348A9C,
        Deaths = 0x84348AD8,
        HeadShots = 0x84348A14,
        Doors = 0x84348AC8,
    }

    public enum BlackOps2ZombiesGameOffset : uint
    {
        ZombiesPoints = 0xC3786FD8,
        /// <summary>
        /// Normal: 0x00, 0xBE, 0x00
        /// Fast: 0x01, 0xBE, 0x00
        /// Very Fast: 0x02, 0xBE, 0x00
        /// </summary>
        PlayerSpeed = 0x1CA4E78
    }
}
