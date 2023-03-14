using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Actions
{
    public class RestoreHealthAction : BattleAction
    {
        public override GameEventType PrePhaseEvent => GameEventType.PreHealPhase;
        public override GameEventType PhaseEvent => GameEventType.HealPhase;

        public RestoreHealthAction(int value, TargetType target) : base(value, target) { }

        protected override async UniTask<int> ExecuteInternal(BattleContext context, GameEventContext prePhaseContext)
        {
            Debug.Log($"Restoring {Value} health to {Target}! (Initial value was: {InitialValue}");
            return Value;
        }
    }
}