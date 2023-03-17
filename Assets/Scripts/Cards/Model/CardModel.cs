using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Vertx.Attributes;

namespace ChiciStudios.BrigittesPlight.Cards.Model
{
    /// <summary>
    /// The base model from which all cards are created. This represents a cards "immutable" data.
    /// These are the data sources from which all <see cref="ChiciStudios.BrigittesPlight.Cards.Entity.CardEntity"/>'s are created.
    /// <para>
    /// Both this and a <see cref="CardController"/> need to be created when adding a new card.
    /// </para>
    /// <seealso cref="ChiciStudios.BrigittesPlight.Cards.Entity.CardEntity"/>
    /// <seealso cref="CardController"/>
    /// </summary>
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
        public string Description => _description;
        public int ChargeCost => _chargeCost;
        public Sprite ArtSprite => _artSprite;
        public CardController Controller => _controller.AsNew();
        #endregion
    }
}