using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Actions
{
    /// <summary>
    /// Base class representing any action that will affect the state of the battle and/or run.
    /// </summary>
    public abstract class BattleAction
    {
        public TargetType Target { get; set; }
        public int InitialValue { get; }
        public int Value { get; set; }

        public virtual async UniTask<int> Execute(BattleContext battleContext, CardEntity castingCard = null)
        {
            try
            {
                var prePhaseContext = await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(PrePhaseEvent, castingCard, this), battleContext);
                var result = await ExecuteInternal(battleContext, prePhaseContext);
                await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(PhaseEvent, castingCard, this), battleContext);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public abstract GameEventType PrePhaseEvent { get; }

        public abstract GameEventType PhaseEvent { get; }

        protected BattleAction(int value, TargetType target)
        {
            InitialValue = value;
            Value = value;
            Target = target;
        }

        protected abstract UniTask<int> ExecuteInternal(BattleContext context, GameEventContext prePhaseContext);

        public override string ToString()
        {
            return $"{nameof(BattleAction)} Details: \nType: {this.GetType().Name} \nTarget: {Target} \nInitial Value:{InitialValue} \nValue: {Value}";
        }
    }
}