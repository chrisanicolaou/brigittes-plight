using System.Globalization;
using System.Text;
using ChiciStudios.BrigittesPlight.Acts;
using ChiciStudios.BrigittesPlight.Extensions;
using ChiciStudios.BrigittesPlight.Rooms;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.BrigittesPlight.Tests.RoomCreation
{
    public class RoomCreationTestSceneController : MonoBehaviour
    {
        [SerializeField] private RoomModel _roomModel;
        private ActContext _actContext;

        [BoxGroup("UI")] [SerializeField] private TextMeshProUGUI _roomTitle;
        [BoxGroup("UI")] [SerializeField] private Transform _encountersParent;
        [BoxGroup("UI")] [SerializeField] private GameObject _encounterTextPrefab;
        [BoxGroup("UI")] [SerializeField] private TextMeshProUGUI _eventLuck;
        [BoxGroup("UI")] [SerializeField] private Button _createNewButton;
        [BoxGroup("UI")] [SerializeField] private Toggle _isFinalRoomToggle;

        private void Start()
        {
            _actContext = new ActContext
            {
                TotalRoomCount = 5,
                CurrentRoomIndex = 1
            };
            _createNewButton.onClick.AddListener(CreateNewRoom);
            _isFinalRoomToggle.onValueChanged.AddListener(SwitchActContext);
        }

        private void CreateNewRoom()
        {
            _encountersParent.DestroyAllChildren();
            var roomContext = RoomFactory.CreateRoomContext(_roomModel, _actContext);

            _roomTitle.text = roomContext.RoomName;
            _eventLuck.text = roomContext.EventLuck.ToString("0.00");

            for (var i = 0; i < roomContext.Encounters.Length; i++)
            {
                var encounter = roomContext.Encounters[i];
                var encounterText = Instantiate(_encounterTextPrefab, _encountersParent)
                    .GetComponent<TextMeshProUGUI>();
                var sb = new StringBuilder();
                sb.Append($"Encounter {i}: ");
                foreach (var encounterOption in encounter.Options)
                {
                    sb.Append($"{encounterOption.Name} ");
                }

                encounterText.text = sb.ToString().TrimEnd();
            }
        }

        private void SwitchActContext(bool value)
        {
            _actContext.CurrentRoomIndex = value ? 4 : 1;
        }
    }
}