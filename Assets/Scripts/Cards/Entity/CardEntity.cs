using System;
using System.Linq;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using ChiciStudios.BrigittesPlight.Cards.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Entity
{
    public class CardEntity
    {
        private string _name;
        private CardController _controller;
        
        public CardEntity(CardModel model)
        {
            _name = model.Name;
            _controller = model.Controller.AsNew();
        }

        public async UniTask Cast(BattleContext battleContext)
        {
            Debug.Log($"Casting card: {_name}");
            await _controller.OnCast(battleContext);
        }
    }
}