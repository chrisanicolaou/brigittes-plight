using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Cards.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.Cards
{
    public class TestCardController : MonoBehaviour
    {
        [SerializeField] private CardModel _cardToTest;
        private CardEntity _cardEntity;
        private BattleContext _mockBattleContext;

        public async UniTask TestPlay()
        {
            _cardEntity = new CardEntity(_cardToTest);
            try
            {
                await _cardEntity.Cast(_mockBattleContext);
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