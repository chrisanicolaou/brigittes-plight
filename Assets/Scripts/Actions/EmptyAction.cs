using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Actions
{
    public class EmptyAction : BattleAction
    {
        public EmptyAction(int value, TargetType target) : base(value, target)
        {
        }

        public override GameEventType PrePhaseEvent => GameEventType.PreDamagePhase;

        public override GameEventType PhaseEvent => GameEventType.DamagePhase;

        protected override UniTask<int> ExecuteInternal(BattleContext context, GameEventContext prePhaseContext)
        {
            return new UniTask<int>(0);
        }
    }
}