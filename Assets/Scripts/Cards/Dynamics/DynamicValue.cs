using System;
using System.Linq;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.GameEvents;

namespace ChiciStudios.BrigittesPlight.Cards.Dynamics
{
    public abstract class DynamicValue
    {
        private static string[] _reservedKeys = new string[]
        {
            "{D}",
            "{H}",
        };
        public bool IsInitialized => Value != -9;
        public abstract string Key { get; }
        public abstract BattleAction ActionToSimulate { get; }
        public int Value { get; set; } = -9;
        
        protected void ValidateKey(string key)
        {
            if (!key.StartsWith('{') || !key.EndsWith('}'))
            {
                throw new ArgumentException($"Format of magic key is invalid. Key: {key}");
            }

            if (_reservedKeys.Any(k => k == key))
            {
                throw new ArgumentException(
                    $"You cannot register a new magic key with the same signature as a reserved key. Key: {key}");
            }
        }
    }
}