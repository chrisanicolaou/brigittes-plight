using System;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    /// <summary>
    /// Base class representing any trigger that will respond to specific game events. (status effects/charms, basically any card that reads ("Whenever X...")
    /// </summary>
    [Serializable]
    public abstract class Trigger
    {
        protected virtual TargetType Target { get; } = TargetType.Enemy;

        private GameEventType _eventSubscribedTo;
        
        protected Trigger(GameEventType eventToSubscribeTo, TargetType target)
        {
            _eventSubscribedTo = eventToSubscribeTo;
            Target = target;
        }

        public bool SubscribedTo(GameEventType eventType)
        {
            return _eventSubscribedTo == eventType;
        }

        public async UniTask OnGameEventFired(BattleContext battleContext, GameEventContext eventContext)
        {
            if (eventContext.IsSimulated && !ShouldExecuteOnSimulation()) return;
            if (eventContext.Action.Target != Target) return;

            await ExecuteTrigger(battleContext, eventContext);
        }

        public abstract UniTask ExecuteTrigger(BattleContext battleContext, GameEventContext eventContext);
        
        // We only want to simulate PrePhase events. This is perhaps a naive implementation for this
        public virtual bool ShouldExecuteOnSimulation()
        {
            return _eventSubscribedTo.ToString().StartsWith("Pre");
        }

        public override string ToString()
        {
            return $"Trigger Details: \nType: {this.GetType().Name}";
        }
    }
}