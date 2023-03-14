using System;
using System.Linq;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using ChiciStudios.BrigittesPlight.Cards.Model;
using ChiciStudios.BrigittesPlight.Cards.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Entity
{
    public class CardEntity
    {
        private UICard _uiCard;
        private string _name;
        private string _description;
        private int _chargeCost;
        private Sprite _sprite;
        private CardController _controller;
        
        public CardEntity(CardModel model)
        {
            _name = model.Name;
            _description = model.Description;
            _chargeCost = model.ChargeCost;
            _controller = model.Controller.AsNew();
            _sprite = model.ArtSprite;
        }

        public void AttachUICard(UICard uiCard)
        {
            if (_uiCard != null)
            {
                Debug.LogError($"Trying to add a new {nameof(UICard)} to {nameof(CardEntity)}, but there is already a UICard attached (name: {_uiCard.name})." +
                               $"{nameof(CardEntity)}'s represent a single instance of a card in game, and should never have more than one view.");
                return;
            }

            _uiCard = uiCard;
            UpdateState();
        }

        // Called at initialization of a UICard, whenever a phase is resolved, or whenever a card is upgraded.
        // For now, just updates the UI. Will also update its own state.
        public void UpdateState()
        {
            if (_uiCard == null) return;
            _uiCard.UpdateName(_name);
            _uiCard.UpdateDescription(_description);
            _uiCard.UpdateChargeCost(_chargeCost);
            _uiCard.UpdateSprite(_sprite);
        }

        public async UniTask Cast(BattleContext battleContext)
        {
            Debug.Log($"Casting card: {_name}");
            await _controller.OnCast(battleContext);
        }
    }
}