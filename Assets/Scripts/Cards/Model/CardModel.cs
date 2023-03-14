using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Vertx.Attributes;

namespace ChiciStudios.BrigittesPlight.Cards.Model
{
    [CreateAssetMenu(fileName = "NewCardModel", menuName = "ScriptableObjects/Cards/CardModel")]
    public class CardModel : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _name;

        [SerializeField] private string _description;

        [SerializeField] private Sprite _artSprite;

        [SerializeField] private int _chargeCost;

        [SerializeReference, ReferenceDropdown] private CardController _controller;
        
        #endregion

        #region Properties

        public string Name => _name;

        public string Description
        {
            get => _description;
            set => _description = value;
        }
        
        public Sprite ArtSprite => _artSprite;
        
        public CardController Controller => _controller;

        #endregion
    }
}