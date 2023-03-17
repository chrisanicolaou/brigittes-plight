using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    public class IncreaseIncomingHealTrigger : Trigger
    {
        private int _healIncreaseAmount;
        public IncreaseIncomingHealTrigger(int healAmount, TargetType target) : base(GameEventType.PreHealPhase, target)
        {
            _healIncreaseAmount = healAmount;
        }

        public override UniTask ExecuteTrigger(BattleContext battleContext, GameEventContext eventContext)
        {
            eventContext.Action.Value += _healIncreaseAmount;
            return UniTask.CompletedTask;
        }
    }
}