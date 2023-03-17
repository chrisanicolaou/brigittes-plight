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
    /// <summary>
    /// The mutable representation of a card during a run, and the singular way a card will be accessed by the rest of the application.
    /// <para>
    /// Whenever a card is created (e.g., when being drafted and/or added to deck), it is done by creating a CardEntity from a given
    /// <see cref="CardModel"/>.
    /// </para>
    /// Responsible for calling methods on the <see cref="CardController"/> when cast and/or updating dynamic descriptions,
    /// and for calling methods on the <see cref="UICard"/> for interacting with the associated GameObject (i.e. animated casts).
    /// <para>See: <see cref="BattleManager"/>'s CastCard method for an example of how this is used.</para>
    /// <seealso cref="CardModel"/>
    /// <seealso cref="UICard"/>
    /// <seealso cref="CardController"/>
    /// </summary>
    public class CardEntity
    {
        private UICard _uiCard;
        private CardModel _model;
        private CardController _controller;
        
        public CardEntity(CardModel model)
        {
            _model = model;
            _controller = model.Controller.AsNew();
        }

        public async UniTask AttachUICard(UICard uiCard)
        {
            if (_uiCard != null)
            {
                Debug.LogError($"Trying to add a new {nameof(UICard)} to {nameof(CardEntity)}, but there is already a UICard attached (name: {_uiCard.name})." +
                               $"{nameof(CardEntity)}'s represent a single instance of a card in game, and should never have more than one view.");
                return;
            }

            _uiCard = uiCard;
            await UpdateState();
        }

        // Called at initialization of a UICard, whenever a phase is resolved, or whenever a card is upgraded.
        // For now, just updates the UI and called in tests. Need to sort this out
        public async UniTask UpdateState(BattleContext battleContext = null)
        {
            if (_uiCard == null) return;
            _uiCard.UpdateName(_model.Name);
            _uiCard.UpdateDescription(battleContext == null ? _model.Description : await _controller.RenderDynamicDescription(_model.Description, battleContext, this));
            _uiCard.UpdateChargeCost(_model.ChargeCost);
            _uiCard.UpdateSprite(_model.ArtSprite);
        }

        public async UniTask OnCast(BattleContext battleContext)
        {
            Debug.Log($"Casting card: {_model.Name}");
            await _controller.OnCast(battleContext, this);
        }
    }
}