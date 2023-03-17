using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Tests.Battle;
using ChiciStudios.BrigittesPlight.Triggers;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Triggers
{
    
    public class TestTriggerController : MonoBehaviour
    {
        [SerializeField] private MockBattleManager _mockBattleManager;
        
        private Trigger _trigger = new RestoreHealthWhenHitTrigger(4, TargetType.Player);

        public void AddTrigger()
        {
            _mockBattleManager.Manager.BattleContext.Triggers.Add(_trigger);
        }
    }
}