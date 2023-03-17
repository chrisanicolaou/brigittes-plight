using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Controller;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Cards.Resources.TestCard
{
    public class TestCardController : CardController
    {
        protected override void Init()
        {
            SetDamage(5, TargetType.Player);
        }
        
        public override async UniTask OnCast(BattleContext battleContext, CardEntity castingCard)
        {
            Debug.Log("TestCard cast!");
            var damageDealtToUseInOtherActions = await new DealDamageAction(Damage.Value, TargetType.Player).Execute(battleContext, castingCard);
        }
    }
}