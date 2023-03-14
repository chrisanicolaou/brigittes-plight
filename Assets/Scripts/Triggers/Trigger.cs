using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    [Serializable]
    public abstract class Trigger
    {
        private GameEventType _eventSubscribedTo;

        protected Trigger(GameEventType eventToSubscribeTo)
        {
            _eventSubscribedTo = eventToSubscribeTo;
        }

        public bool SubscribedTo(GameEventType eventType)
        {
            return _eventSubscribedTo == eventType;
        }

        public async UniTask OnGameEventFired(BattleContext battleContext, GameEventContext eventContext)
        {
            if (eventContext.IsSimulated && !ShouldExecuteOnSimulation()) return;

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