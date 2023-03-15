using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Resources.TestCard
{
    public class TestCardController : CardController
    {
        public override async UniTask OnCast(BattleContext battleContext)
        {
            Debug.Log("TestCard cast!");
            var dealDamage = new DealDamageAction(5, TargetType.Player);
            await dealDamage.Execute(battleContext);
        }

        public override CardController AsNew()
        {
            return new TestCardController();
        }
    }
}