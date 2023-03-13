using ChiciStudios.BrigittesPlight.Encounters;

namespace ChiciStudios.BrigittesPlight.Rooms
{
    public class RoomContext
    {
        public string RoomName { get; set; }
        public int TotalEncounters { get; set; }
        public int CurrentEncounterIndex { get; set; }
        public Encounter[] Encounters { get; set; }
        public float EventLuck { get; set; }
    }
}