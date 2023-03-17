using System;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ChiciStudios.BrigittesPlight.Cards.UI
{
    public class UICardBuilder
    {
        private const string CardPrefabPath = "Cards/UICard";
        private static GameObject _cardPrefab;

        private CardEntity _cardEntity;
        private Transform _parent;
        
        public UICardBuilder() { }

        public UICardBuilder(CardEntity cardEntity, Transform parent)
        {
            _cardEntity = cardEntity;
            _parent = parent;
        }

        public UICardBuilder SetEntity(CardEntity cardEntity)
        {
            _cardEntity = cardEntity;
            return this;
        }

        public UICardBuilder SetParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        
        public async UniTask Build()
        {
            if (_cardEntity == null)
            {
                Debug.LogError($"Cannot correctly create UICards without a {nameof(CardEntity)} specified. Aborting");
                return;
            }
            
            if (_parent == null)
            {
                Debug.LogError("Cannot correctly create UICards without a parent specified. Aborting");
                return;
            }
            
            _cardPrefab ??= UnityEngine.Resources.Load<GameObject>(CardPrefabPath);

            if (_cardPrefab == null)
            {
                Debug.LogError($"Cannot load card prefab from the path specified: {CardPrefabPath}. {nameof(UICardBuilder)} cannot build cards until this is resolved.");
                return;
            }
            
            var uiCardGo = Object.Instantiate(_cardPrefab, _parent, false);

            var uiCard = uiCardGo.GetComponent<UICard>();
            if (uiCard == null)
            {
                Debug.LogError($"Cannot find {nameof(UICard)} component on {uiCardGo.name}. {nameof(UICardBuilder)} cannot build cards correctly without this component!");
                Object.Destroy(uiCardGo);
                return;
            }
            
            await _cardEntity.AttachUICard(uiCard);
        }
    }
}