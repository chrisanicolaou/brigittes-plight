using System;
using System.Linq;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Entity
{
    public class CardEntity
    {
        private string _name;
        private readonly BattleAction[] _actions;
        
        public CardEntity(CardModel model)
        {
            _name = model.Name;
            _actions = model.Actions.ToArray();
        }

        public async UniTask Cast(BattleContext battleContext)
        {
            Debug.Log($"Casting card: {_name}");
            var castResult = new CastReserve();
            foreach (var action in _actions)
            {
                Debug.Log($"Attempting to execute {action.GetType().Name}");
                if (!action.CanExecute(battleContext, this, castResult))
                {
                    Debug.Log($"{action.GetType().Name} cancelled - condition not met!");
                }
                await action.Execute(battleContext, this, castResult);
            }
        }
    }
}