using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.BrigittesPlight.Cards.UI
{
    public class UICard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _chargeCost;
        [SerializeField] private Image _img;

        public void UpdateName(string cardName)
        {
            _name.text = cardName;
        }

        public void UpdateDescription(string description)
        {
            _description.text = description;
        }

        public void UpdateChargeCost(int chargeCost)
        {
            _chargeCost.text = chargeCost.ToString();
        }

        public void UpdateSprite(Sprite sprite)
        {
            _img.sprite = sprite;
        }
    }
}