using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.GameEvents;

namespace ChiciStudios.BrigittesPlight.Cards.Dynamics
{
    public class HealValue : DynamicValue
    {
        public override string Key => "{H}";
        public override BattleAction ActionToSimulate { get; }

        public HealValue(int value, TargetType target)
        {
            Value = value;
            ActionToSimulate = new RestoreHealthAction(value, target);
        }
    }
}