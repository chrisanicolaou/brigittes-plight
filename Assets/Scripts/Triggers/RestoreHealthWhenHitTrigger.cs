using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    public class RestoreHealthWhenHitTrigger : Trigger
    {
        private int _restoreAmount;

        public RestoreHealthWhenHitTrigger(int amount, TargetType targetType) : base(GameEventType.DamagePhase, targetType)
        {
            _restoreAmount = amount;
        }

        public override async UniTask ExecuteTrigger(BattleContext battleContext, GameEventContext eventContext)
        {
            await new RestoreHealthAction(_restoreAmount, Target).Execute(battleContext);
        }
    }
}