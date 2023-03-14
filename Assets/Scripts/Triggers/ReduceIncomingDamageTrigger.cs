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
        private TargetType _target;
        
        public ReduceIncomingDamageTrigger(int reduceAmount, TargetType target) : base(GameEventType.PreDamagePhase)
        {
            _reduceAmount = reduceAmount;
            _target = target;
        }

        public override async UniTask OnGameEventFired(BattleContext battleContext, GameEventContext eventContext)
        {
            if (eventContext.Action.Target != _target) return;
            
            eventContext.Action.Value = Mathf.Max(0, eventContext.Action.Value - _reduceAmount);
            return;
        }
    }
}