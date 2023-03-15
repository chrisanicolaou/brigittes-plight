using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    public class ReduceIncomingDamageTrigger : Trigger
    {
        private int _reduceAmount;
        
        public ReduceIncomingDamageTrigger(int reduceAmount, TargetType target) : base(GameEventType.PreDamagePhase, target)
        {
            _reduceAmount = reduceAmount;
        }

        public override UniTask ExecuteTrigger(BattleContext battleContext, GameEventContext eventContext)
        {
            eventContext.Action.Value = Mathf.Max(0, eventContext.Action.Value - _reduceAmount);
            return UniTask.CompletedTask;
        }
    }
}