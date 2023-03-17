using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Dynamics;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Cards.Model;
using ChiciStudios.BrigittesPlight.Cards.Resources.TestCard;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Controller
{
    /// <summary>
    /// The base class from which all newly created cards will need to derive from.
    /// Notably contains:
    /// <list type="bullet">
    /// <item>
    /// <description>getters/setters for <see cref="DynamicValue"/>'s that will need to be included in dynamic descriptions</description>
    /// </item>
    /// <item>
    /// <description>An <see cref="Init"/> method for initializing dynamic values</description>
    /// </item>
    /// <item>
    /// <description>An <see cref="OnCast"/> method for cards to implement their own effects</description>
    /// </item>
    /// </list>
    /// See <see cref="TestCardController"/> for an example of how a card effect can be implemented.
    /// <seealso cref="CardEntity"/>
    /// <seealso cref="CardModel"/>
    /// <seealso cref="DynamicValue"/>
    /// </summary>
    [Serializable]
    public abstract class CardController
    {
        private bool _isDamageSet;
        private bool _isHealSet;

        private List<DynamicValue> _dynamicValues = new();

        public DynamicValue Damage => _dynamicValues.FirstOrDefault(d => d.Key == "{D}");
        public DynamicValue Heal => _dynamicValues.FirstOrDefault(d => d.Key == "{H}");
        /// <summary>
        /// The abstract method controlling what a card does when cast.
        /// </summary>
        /// <param name="battleContext">The battle context, for cards that may need battle-specific information for their effects.
        /// For example, if a card reads "Deal 5 damage for each card in your hand", it could be implemented like so:
        /// <code>
        /// for (var i = 0; i &lt; battleContext.CardsInHand; i++)
        /// {
        ///     await new DealDamageAction(Damage.Value, TargetType.Player).Execute(battleContext, castingCard);
        /// }
        /// </code></param>
        /// <param name="castingCard">The <see cref="CardEntity"/> casting this effect. Used for cards that may need card-specific information.
        /// (e.g., if a card reads "Deal damage equal to this card's charge cost").</param>
        /// <returns></returns>
        public abstract UniTask OnCast(BattleContext battleContext, CardEntity castingCard);

        /// <summary>
        /// Used by <see cref="CardEntity"/> when a card is created. I'm not sure why yet, just felt right.
        /// </summary>
        /// <returns></returns>
        public CardController AsNew()
        {
            var cardController = (CardController)Activator.CreateInstance(this.GetType());
            cardController.Init();
            return cardController;
        }

        /// <summary>
        /// Required for all implementations of <see cref="CardController"/>. Responsible for setting initial values, of which are
        /// required to make dynamic descriptions work correctly.
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// The core idea behind how I'm planning for Dynamic Descriptions to work is in here, by simulating <see cref="BattleAction"/>s. The idea is as follows:
        /// <list type="number">
        /// <item>
        /// <description><see cref="CardModel"/>s will write descriptions by replacing any value with keys. For example - "Deal {D} damage".</description>
        /// </item>
        /// <item>
        /// <description>The associated <see cref="CardController"/>s will set values to replace these keys. <see cref="DamageValue"/> for damage, <see cref="HealValue"/> for heal.
        /// <see cref="RegisterMagicValue"/> for anything else.</description>
        /// </item>
        /// <item>
        /// <description>This method will grab any keys defined in the model's description, execute a simulated event based off of the action, and
        /// string replace the result.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="description">Fed from the <see cref="CardModel"/> through the <see cref="CardEntity"/>.</param>
        /// <param name="battleContext">To pass through to the corresponding simulated <see cref="GameEventContext"/> generated when executing the event.
        /// Required for making sure battle triggers are considered</param>
        /// <param name="castingCard">The <see cref="CardEntity"/> calling for a re-rendered description, to pass through to the <see cref="GameEventContext"/></param>
        /// <returns>A string of the correct dynamic description</returns>
        public virtual async UniTask<string> RenderDynamicDescription(string description, BattleContext battleContext, CardEntity castingCard)
        {
            var regex = new Regex(@"{[A-Z]+}", RegexOptions.Compiled);
            var matches = regex.Matches(description);
            var dynamicDescription = description;

            foreach (Match match in matches)
            {
                var matchingDynamicValue = _dynamicValues.FirstOrDefault(d => d.Key == match.Value);
                if (matchingDynamicValue is not { IsInitialized: true })
                {
                    Debug.LogError($"Dynamic description key {match.Value} was not correctly initialized in {this.GetType().Name}");
                    continue;
                }

                matchingDynamicValue.ActionToSimulate.Value = matchingDynamicValue.Value;
                await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(
                    matchingDynamicValue.ActionToSimulate.PrePhaseEvent, 
                    castingCard, 
                    matchingDynamicValue.ActionToSimulate, true), 
                    battleContext);
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