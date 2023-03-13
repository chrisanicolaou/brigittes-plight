using ChiciStudios.BrigittesPlight.Enemies;
using ChiciStudios.BrigittesPlight.Rooms;

namespace ChiciStudios.BrigittesPlight.Acts
{
    public class ActContext
    {
        public int TotalRoomCount { get; set; }
        public int CurrentRoomIndex { get; set; }
        public bool IsFinalRoom => CurrentRoomIndex == TotalRoomCount - 1;
        
        public EnemyModel ActBoss { get; set; }
    }
}