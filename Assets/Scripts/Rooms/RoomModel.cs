using ChiciStudios.BrigittesPlight.Enemies;
using NaughtyAttributes;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Rooms
{
    [CreateAssetMenu(fileName = "NewRoomModel", menuName = "ScriptableObjects/Rooms/RoomModel")]
    public class RoomModel : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] [ReorderableList] private EnemyModel[] _enemyModels;
        [SerializeField] private int _totalEncounters;
        [SerializeField] private int _innCount;
        [SerializeField] private int _merchantCount;

        // [SerializeField] private EventModel[]? _eventPool;
        [SerializeField] private int _eventCount;
        [SerializeField] [Range(0.1f, 100f)] private float _eventLuck;
        
        public string Name => _name;
        public EnemyModel[] EnemyModels => _enemyModels;
        public int TotalEncounters => _totalEncounters;
        public int InnCount => ClampSpecialEncounterCount(_innCount);
        public int MerchantCount => ClampSpecialEncounterCount(_merchantCount);

        // public EventModel[]? EventPool => _eventPool;
        public int EventCount => ClampSpecialEncounterCount(_eventCount);
        public float EventLuck => _eventLuck;
        
        protected virtual int ClampSpecialEncounterCount(int count)
        {
            return Mathf.Min(count, _totalEncounters - 1);
        }
    }
}