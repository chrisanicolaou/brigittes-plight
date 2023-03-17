using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using ChiciStudios.BrigittesPlight.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = System.Object;

namespace ChiciStudios.BrigittesPlight.Battle
{
    public class BattleManager
    {
        private BattleContext _battleContext;

        // THIS WILL BE REPLACED ONCE I KNOW HOW TO KEEP TRACK OF CARDS IN PLAY. FOR NOW, THEY ARE REGISTERED HERE IN TESTS
        public List<CardEntity> CardsInHand { get; set; } = new();
        public BattleContext BattleContext => _battleContext;
        
        public BattleManager()
        {
            _battleContext = new BattleContext(this);
        }
        
        public async UniTask CastCard(CardEntity card)
        {
            try
            {
                await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(GameEventType.PreCastPhase, card), _battleContext);
                await card.OnCast(_battleContext);
                await GameEventExecutor.Instance.FireGameEvent(new GameEventContext(GameEventType.CastPhase, card), _battleContext);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        // THIS IS WHERE YOU WERE :). Now is the time to figure out how 
        public async UniTask EvaluateBattleState()
        {
            CardEntity[] cardsInHand = GetCardsInHand();
        }

        private CardEntity[] GetCardsInHand()
        {
            return CardsInHand.ToArray();
        }
    }
}