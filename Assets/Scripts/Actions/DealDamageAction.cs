using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Actions
{
    public class DealDamageAction : BattleAction
    {
        public override GameEventType PrePhaseEvent => GameEventType.PreDamagePhase;
        public override GameEventType PhaseEvent => GameEventType.DamagePhase;

        public DealDamageAction(int value, TargetType target) : base(value, target) { }
        
        protected override async UniTask<int> ExecuteInternal(BattleContext context, GameEventContext prePhaseContext)
        {
            Debug.Log($"Dealing {Value} damage to {Target}! (Initial value was: {InitialValue})");
            return Value;
        }
    }
}