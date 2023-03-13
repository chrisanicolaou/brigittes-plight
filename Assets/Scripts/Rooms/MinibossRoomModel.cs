using ChiciStudios.BrigittesPlight.Enemies;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Rooms
{
    [CreateAssetMenu(fileName = "NewMinibossRoomModel", menuName = "ScriptableObjects/Rooms/MinibossRoomModel")]
    public class MinibossRoomModel : RoomModel
    {
        [SerializeField] private EnemyModel _minibossModel;

        protected override int ClampSpecialEncounterCount(int count)
        {
            return Mathf.Min(count, TotalEncounters - 2);
        }
    }
}   