using System.Collections.Generic;
using System.Linq;
using ChiciStudios.BrigittesPlight.GameEvents;
using ChiciStudios.BrigittesPlight.Triggers;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Battle
{
    public class BattleContext
    {
        private BattleManager _manager;
        private List<Trigger> _triggers;
        public BattleManager Manager => _manager;
        public List<Trigger> Triggers => _triggers;

        public BattleContext(BattleManager manager)
        {
            _manager = manager;
            _triggers = new List<Trigger>();
        }

        public Trigger[] GetSubscribingTriggers(GameEventType eventContextType)
        {
            Debug.Log($"Fetching triggers subscribed to: {eventContextType}");
            return _triggers.Where(t => t.SubscribedTo(eventContextType)).ToArray();
        }
    }
}