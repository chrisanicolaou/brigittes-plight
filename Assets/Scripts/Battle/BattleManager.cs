using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using ChiciStudios.BrigittesPlight.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Battle
{
    public class BattleManager
    {
        private BattleContext _battleContext;
        private readonly List<Trigger> _triggers = new();
        private int _triggerDepth;

        public BattleContext BattleContext => _battleContext;
        
        public BattleManager()
        {
            _battleContext = new BattleContext(this);
        }
        
        public async UniTask CastCard(CardEntity card)
        {
            try
            {
                await FireGameEvent(new GameEventContext(GameEventType.PreCastPhase));
                await card.Cast(_battleContext);
                await FireGameEvent(new GameEventContext(GameEventType.CastPhase));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public void AddTrigger(Trigger trigger)
        {
            Debug.Log($"Adding trigger: {trigger}");
            _triggers.Add(trigger);
        }

        private Trigger[] GetSubscribingTriggers(GameEventType eventType)
        {
            Debug.Log($"Fetching triggers subscribed to: {eventType}");
            return _triggers.Where(t => t.SubscribedTo(eventType)).ToArray();
        }

        public async UniTask<GameEventContext> FireGameEvent(GameEventContext eventContext)
        {
            _triggerDepth++;
            if (_triggerDepth > 256) throw new Exception("Recursive event loop detected!");
            Debug.Log($"Firing GameEvent: {eventContext.Type}");
            var triggers = GetSubscribingTriggers(eventContext.Type);
            foreach (var trigger in triggers)
            {
                Debug.Log($"Trigger found! Firing {trigger}");
                await trigger.OnGameEventFired(_battleContext, eventContext);
                Debug.Log($"Resolving back to: {eventContext}");
            }
            return eventContext;
        }
    }
}