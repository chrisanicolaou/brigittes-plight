using System;
using System.Collections.Generic;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Cards.Model;
using ChiciStudios.BrigittesPlight.Cards.UI;
using NaughtyAttributes;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Tests.UICardCreation
{
    public class TestUICardCreationController : MonoBehaviour
    {
        [SerializeField] private TestCardModelInfo[] _cardModelInfosToCreate;
        [SerializeField] private Transform _mockParent;

        private void Start()
        {
            var cardEntities = GenerateEntities();
            var cardBuilder = new UICardBuilder();
            cardBuilder.SetParent(_mockParent);

            foreach (var entity in cardEntities)
            {
                cardBuilder.SetEntity(entity).Build();
            }
        }

        private CardEntity[] GenerateEntities()
        {
            var cardEntities = new List<CardEntity>();
            foreach (var cardModelInfo in _cardModelInfosToCreate)
            {
                for (var i = 0; i < cardModelInfo.timesToCreate; i++)
                {
                    cardEntities.Add(new CardEntity(cardModelInfo.cardModel));
                }
            }

            return cardEntities.ToArray();
        }

        [Serializable]
        private class TestCardModelInfo
        {
            // ReSharper disable once InconsistentNaming
            public CardModel cardModel;
            // ReSharper disable once InconsistentNaming
            public int timesToCreate;
        }
    }
}