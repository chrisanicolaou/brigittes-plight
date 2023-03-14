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

        public abstract UniTask OnGameEventFired(BattleContext battleContext, GameEventContext eventContext);

        public override string ToString()
        {
            return $"Trigger Details: \nType: {this.GetType().Name}";
        }
    }
}