using System;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.GameEvents;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Dynamics
{
    public class MagicValue : DynamicValue
    {
        public override string Key { get; } = "{M}";

        public override BattleAction ActionToSimulate { get; }

        public MagicValue(string key, int value, BattleAction actionToSimulate)
        {
            try
            {
                ValidateKey(key);
            }
            catch (ArgumentException e)
            {
                Debug.LogException(e);
                return;
            }
            
            Key = key;
            Value = value;
            ActionToSimulate = actionToSimulate;
        }
    }
}