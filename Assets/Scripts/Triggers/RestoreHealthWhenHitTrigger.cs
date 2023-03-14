using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Triggers
{
    public class RestoreHealthWhenHitTrigger : Trigger
    {
        private int _restoreAmount;
        private TargetType _target;

        public RestoreHealthWhenHitTrigger(int amount, TargetType targetType) : base(GameEventType.DamagePhase)
        {
            _restoreAmount = amount;
            _target = targetType;
        }

        public override async UniTask OnGameEventFired(BattleContext battleContext, GameEventContext eventContext)
        {
            if (eventContext.Action.Target != _target) return;

            await new RestoreHealthAction(_restoreAmount, _target).Execute(battleContext);
        }
    }
}