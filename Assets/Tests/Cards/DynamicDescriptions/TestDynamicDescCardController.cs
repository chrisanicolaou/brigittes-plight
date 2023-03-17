using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using ChiciStudios.BrigittesPlight.Cards.Dynamics;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Tests.Cards.DynamicDescriptions
{
    public class TestDynamicDescCardController : CardController
    {
        public override async UniTask OnCast(BattleContext battleContext, CardEntity castingCard)
        {
            return;
        }

        protected override void Init()
        {
            SetDamage(10, TargetType.Player);
            SetHealth(5, TargetType.Player);
        }
    }
}