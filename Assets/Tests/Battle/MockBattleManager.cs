using ChiciStudios.BrigittesPlight.Battle;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Battle
{
    [ExecuteInEditMode]
    public class MockBattleManager : MonoBehaviour
    {
        public BattleManager Manager { get; } = new BattleManager();
    }
}