using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Cards.Model;
using ChiciStudios.BrigittesPlight.Cards.UI;
using ChiciStudios.BrigittesPlight.Tests.Battle;
using ChiciStudios.BrigittesPlight.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ChiciStudios.BrigittesPlight.Tests.Cards.DynamicDescriptions
{
    public class TestDynamicDescController : MonoBehaviour
    {
        [SerializeField] private Button _addDmgTrigger;
        [SerializeField] private Button _addHealTrigger;
        [SerializeField] private Button _renderDescBtn;
        [SerializeField] private CardModel _model;
        [SerializeField] private Transform _canvasTransform;

        private CardEntity _cardEntity;
        private BattleManager mockManager = new BattleManager();
        private BattleContext _battleContext;

        private void Start()
        {
            _battleContext = new BattleContext(mockManager);
            _cardEntity = new CardEntity(_model);
            new UICardBuilder(_cardEntity, _canvasTransform).Build().Forget();
            
            _addDmgTrigger.onClick.AddListener(AddDamageTrigger);
            _addHealTrigger.onClick.AddListener(AddHealTrigger);
            _renderDescBtn.onClick.AddListener(UpdateState);
        }

        private void AddHealTrigger()
        {
            _battleContext.Triggers.Add(new IncreaseIncomingHealTrigger(2, TargetType.Player));
        }

        private void UpdateState()
        {
            _cardEntity.UpdateState(_battleContext).Forget();
        }

        private void AddDamageTrigger()
        {
            _battleContext.Triggers.Add(new ReduceIncomingDamageTrigger(1, TargetType.Player));
        }
    }
}