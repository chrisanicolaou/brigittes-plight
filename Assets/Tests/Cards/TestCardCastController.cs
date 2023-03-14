using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Cards.Model;
using ChiciStudios.BrigittesPlight.Tests.Battle;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Cards
{
    public class TestCardCastController : MonoBehaviour
    {
        [SerializeField] private MockBattleManager _mockBattleManager;
        [SerializeField] private CardModel _cardToTest;
        private CardEntity _cardEntity;
        private BattleManager _battleManager;

        public async UniTask TestPlay()
        {
            _battleManager ??= _mockBattleManager.Manager;
            _cardEntity = new CardEntity(_cardToTest);
            try
            {
                await _battleManager.CastCard(_cardEntity);
                Debug.Log("Success!");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to play card!");
                Debug.LogException(e);
            }
        }
    }
}