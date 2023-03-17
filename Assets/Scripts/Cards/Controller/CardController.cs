using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Dynamics;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Controller
{
    [Serializable]
    public abstract class CardController
    {
        private bool _isDamageSet;
        private bool _isHealSet;

        private List<DynamicValue> _dynamicValues = new();

        public DynamicValue Damage => _dynamicValues.FirstOrDefault(d => d.Key == "{D}");
        public DynamicValue Heal => _dynamicValues.FirstOrDefault(d => d.Key == "{H}");
        public abstract UniTask OnCast(BattleContext battleContext, CardEntity castingCard);

        public CardController AsNew()
        {
            var cardController = (CardController)Activator.CreateInstance(this.GetType());
            cardController.Init();
            return cardController;
        }

        protected abstract void Init();

        public virtual async UniTask<string> RenderDynamicDescription(string description, BattleContext battleContext, CardEntity castingCard)
        {
            // Basically pulls out all occurrences of any letters inside "{ }" from the description (i.e. "{D}").
            var regex = new Regex(@"{[A-Z]+}", RegexOptions.Compiled);
            var matches = regex.Matches(description);
            
            // Creates a copy of the original description
            var dynamicDescription = description;

            // For every match:
            foreach (Match match in matches)
            {
                // Finds the dynamic value that corresponds to that specific key
                var matchingDynamicValue = _dynamicValues.FirstOrDefault(d => d.Key == match.Value);
                // If one doesn't exist, it means something like "{D}" exists in the description, but hasn't been programmed into that card's script
                if (matchingDynamicValue is not { IsInitialized: true })
                {
                    Debug.LogError($"Dynamic description key {match.Value} was not correctly initialized in {this.GetType().Name}");
                    continue;
                }

                // Simulates what would happen if that specific action was played - i.e, for keys of "{D}", plays a damage card with a pre-specified target.
                matchingDynamicValue.ActionToSimulate.Value = matchingDynamicValue.Value;
                await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(
                    matchingDynamicValue.ActionToSimulate.PrePhaseEvent, 
                    castingCard, 
                    matchingDynamicValue.ActionToSimulate, true), 
                    battleContext);
                // Once that card has been "played", it replaces the description key (match.Value) with the result of playing that action.
                dynamicDescription = dynamicDescription.Replace(match.Value, matchingDynamicValue.ActionToSimulate.Value.ToString());
                matchingDynamicValue.ActionToSimulate.Value = matchingDynamicValue.Value;
            }

            return dynamicDescription;
        }

        protected void SetDamage(int value, TargetType target)
        {
            if (_isDamageSet)
            {
                var damageValue = _dynamicValues.FirstOrDefault(d => d.Key == "{D}");
                if (damageValue == null) return;
                damageValue.Value = value;
                damageValue.ActionToSimulate.Target = target;
            }
            else
            {
                var damageValue = new DamageValue(value, target);
                _dynamicValues.Add(damageValue);
                _isDamageSet = true;
            }
        }

        protected void SetHealth(int value, TargetType target)
        {
            if (_isHealSet)
            {
                var healValue = _dynamicValues.FirstOrDefault(d => d.Key == "{H}");
                if (healValue == null) return;
                healValue.Value = value;
                healValue.ActionToSimulate.Target = target;
            }
            else
            {
                var healValue = new HealValue(value, target);
                _dynamicValues.Add(healValue);
                _isHealSet = true;
            }
        }

        protected void RegisterMagicValue(MagicValue magicValue)
        {
            _dynamicValues.Add(magicValue);
        }

        protected DynamicValue GetMagicValue(string key)
        {
            return _dynamicValues.FirstOrDefault(d => d.Key == key);
        }
    }
}