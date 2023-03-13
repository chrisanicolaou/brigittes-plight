using ChiciStudios.BrigittesPlight.Actions;
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

        [SerializeReference][ReferenceDropdown] private BattleAction[] _actions;

        #endregion

        #region Properties

        public string Name => _name;

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public BattleAction[] Actions => _actions;

        public Sprite ArtSprite => _artSprite;

        #endregion
    }
}