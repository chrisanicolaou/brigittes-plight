using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Actions
{
    public abstract class BattleAction
    {
        public TargetType Target { get; set; }
        public int InitialValue { get; }
        public int Value { get; set; }

        public virtual async UniTask<int> Execute(BattleContext context)
        {
            try
            {
                var prePhaseContext = await context.Manager.FireGameEvent(new GameEventContext(PrePhaseEvent, this));
                var result = await ExecuteInternal(context, prePhaseContext);
                await context.Manager.FireGameEvent(new GameEventContext(PhaseEvent, this));
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