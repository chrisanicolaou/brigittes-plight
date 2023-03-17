using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.GameEvents;

namespace ChiciStudios.BrigittesPlight.Cards.Dynamics
{
    public class DamageValue : DynamicValue
    {
        public override string Key => "{D}";
        public override BattleAction ActionToSimulate { get; }

        public DamageValue(int value, TargetType target)
        {
            Value = value;
            ActionToSimulate = new DealDamageAction(value, target);
        }
    }
}