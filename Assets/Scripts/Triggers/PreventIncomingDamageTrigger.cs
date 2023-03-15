using System;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    /// <summary>
    /// An example of how individual triggers can control whether or not they will simulate (i.e be included in pre-damage calculations for dynamic descriptions).
    /// </summary>
    public class PreventIncomingDamageTrigger : Trigger
    {
        public PreventIncomingDamageTrigger(GameEventType eventToSubscribeTo, TargetType target) : base(eventToSubscribeTo, target) { }
        
        public override UniTask ExecuteTrigger(BattleContext battleContext, GameEventContext eventContext)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldExecuteOnSimulation() => false;
    }
}